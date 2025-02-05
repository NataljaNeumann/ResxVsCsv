﻿/* MIT License

Copyright (c) 2025 NataljaNeumann

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net;
using System.Text;


namespace ResxVsCsv
{
    //*******************************************************************************************************
    /// <summary>
    /// The program class
    /// </summary>
    //*******************************************************************************************************
    class Program
    {
        //===================================================================================================
        /// <summary>
        /// Columns of the CSV
        /// </summary>
        enum eColumn
        {
            Culture,
            Name,
            Value,
            Comment
        }


        //===================================================================================================
        /// <summary>
        /// Main method of the program, accepts user input
        /// </summary>
        /// <param name="aArgs">Command line arguments</param>
        //===================================================================================================
        static void Main(
            string[] aArgs
            )
        {
            try
            {
                string strPattern = "*.*";
                string strDirectory = Directory.GetCurrentDirectory();
                string strToResx = null;
                string strSortByName = "no";
                string strAPIKey = null;
                string strTranslationService = null;
                bool bHelp = aArgs.Length == 0;

                // Parse command line arguments
                for (int i = 0; i < aArgs.Length; i++)
                {
                    switch (aArgs[i])
                    {
                        case "--pattern":
                            if (i + 1 < aArgs.Length)
                            {
                                strPattern = aArgs[++i];
                            }
                            break;
                        case "--directory":
                            if (i + 1 < aArgs.Length)
                            {
                                strDirectory = aArgs[++i];
                            }
                            break;
                        case "--translator":
                            if (i + 1 < aArgs.Length)
                            {
                                strTranslationService = aArgs[++i];
                            }
                            break;
                        case "--toresx":
                            if (i + 1 < aArgs.Length)
                            {
                                strToResx = aArgs[++i];
                            }
                            break;
                        case "--sortbyname":
                            if (i + 1 < aArgs.Length)
                            {
                                strSortByName = aArgs[++i];
                            }
                            break;
                        case "--apikey":
                            if (i + 1 < aArgs.Length)
                            {
                                strAPIKey = aArgs[++i];
                            }
                            break;
                        case "--help":
                        case "-?":
                        case "/?":
                        case "--licence":
                        case "--license":
                            System.Console.WriteLine("Copyright (c) 2025 NataljaNeumann");
                                System.Console.WriteLine();
                            System.Console.WriteLine("Permission is hereby granted, free of charge, to any person obtaining a copy");
                            System.Console.WriteLine("of this software and associated documentation files (the \"Software\"), to deal");
                            System.Console.WriteLine("in the Software without restriction, including without limitation the rights");
                            System.Console.WriteLine("to use, copy, modify, merge, publish, distribute, sublicense, and/or sell");
                            System.Console.WriteLine("copies of the Software, and to permit persons to whom the Software is");
                            System.Console.WriteLine("furnished to do so, subject to the following conditions:");
                            System.Console.WriteLine();
                            System.Console.WriteLine("The above copyright notice and this permission notice shall be included in all");
                            System.Console.WriteLine("copies or substantial portions of the Software.");
                            System.Console.WriteLine();
                            System.Console.WriteLine("THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR");
                            System.Console.WriteLine("IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,");
                            System.Console.WriteLine("FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE");
                            System.Console.WriteLine("AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER");
                            System.Console.WriteLine("LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,");
                            System.Console.WriteLine("OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE");
                            System.Console.WriteLine("SOFTWARE.");

                            System.Console.WriteLine(
                                "For conversion to CSV: ResxVsCsv --directory <dir> --pattern <pattern> [--sortbyname yes]");
                            System.Console.WriteLine(
                                "For translation: ResxVsCsv --directory <dir> --pattern <pattern> --translator <google|microsoft> --apikey <key> [--sortbyname yes] ");
                            System.Console.WriteLine(
                                "For updating .Resx files: ResxVsCsv --directory <dir> --toresx <resources.csv>");
                            return;
                    }
                }

                if (strToResx != null)
                {
                    // the transformation is back from CSV to RESX

                    // read the CSV file
                    List<Entry> oEntries = new List<Entry>(
                        ReadCsvFile(System.IO.Path.Combine(strDirectory, strToResx)));
                    var oDistinctCultures = oEntries
                        .Where(x => !string.IsNullOrEmpty(x.Culture))
                        .Select(x => x.Culture)
                        .Distinct()
                        .ToList();

                    // update single resx files
                    foreach (string strCulture in oDistinctCultures)
                    {
                        string strResxCulture = strCulture.Equals("(default)") ? "" : "." + strCulture;
                        UpdateResxFile(oEntries.Where(x => x.Culture.Equals(strCulture)),
                            System.IO.Path.Combine(strDirectory, strToResx.Replace(".csv", strResxCulture + ".resx")));
                    }
                }
                else
                {
                    // the transformation is forward from RESX to CSV

                    // Get files matching the pattern
                    var aFiles = Directory.GetFiles(strDirectory, strPattern, SearchOption.TopDirectoryOnly);

                    List<Entry> oAllEntries = new List<Entry>();

                    // the base file name for CSV creation
                    string strBaseName = "";

                    // Print selected files
                    foreach (var file in aFiles)
                    {
                        // calc components of the file name
                        string strFileName = file.Substring(file.LastIndexOf('\\') + 1);
                        string strFileNameWithoutExt = strFileName.Substring(0, strFileName.LastIndexOf('.'));
                        string strCulture = strFileNameWithoutExt.Contains('.') ? strFileNameWithoutExt.Substring(strFileNameWithoutExt.LastIndexOf('.') + 1) : "(default)";
                        strBaseName = strFileNameWithoutExt.Contains('.') ? strFileNameWithoutExt.Substring(0, strFileNameWithoutExt.LastIndexOf('.')) : strFileNameWithoutExt;
                        // read the resx file and add entries to the overall list
                        oAllEntries.AddRange(ReadResxFile(file, strCulture));
                    }

                    // find distinct cultures
                    var distinctCultures = oAllEntries
                        .Where(x => !string.IsNullOrEmpty(x.Culture))
                        .Select(x => x.Culture)
                        .Distinct()
                        .ToList();

                    // find distinct names for values
                    var distinctNames = oAllEntries
                        .Where(x => !string.IsNullOrEmpty(x.Name))
                        .Select(x => x.Name)
                        .Distinct()
                        .ToList();

                    // collect all entries in a single dictionary for fast access
                    Dictionary<Entry, Entry> oEntriesDictionary = new Dictionary<Entry, Entry>();

                    foreach (Entry oEntry in oAllEntries)
                        oEntriesDictionary[oEntry] = oEntry;

                    // Now reorder the elements in needed order for output
                    List<Entry> outputlist = new List<Entry>();

                    if (strSortByName.Equals("yes"))
                    {
                        foreach (string strName in distinctNames)
                            foreach (string strCulture in distinctCultures)
                            {
                                Entry oEntry;
                                if (oEntriesDictionary.TryGetValue(
                                    new Entry { Culture = strCulture, Name = strName }, out oEntry))
                                {
                                    outputlist.Add(oEntry);
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(strAPIKey))
                                    {
                                        outputlist.Add(new Entry
                                        {
                                            Comment = "Empty space for " + strCulture + " - " + strName
                                        });
                                    }
                                    else
                                    {
                                        var translations = new List<Translation>();

                                        string strLocalizedTextVariant = null;
                                        Entry oFoundEntry = null;
                                        if (oEntriesDictionary.TryGetValue(
                                            new Entry { Culture = "(default)", Name = strName }, out oFoundEntry))
                                        {
                                            strLocalizedTextVariant = oFoundEntry.Value;
                                            if (!string.IsNullOrEmpty(strLocalizedTextVariant))
                                                translations.Add(new Translation
                                                {
                                                    Language = "en",
                                                    Text = strLocalizedTextVariant
                                                });
                                        };


                                        foreach (string strSourceCulture in new string[] { "es", "de", "pt", "it", "en", "fr" })
                                        {
                                            if (oEntriesDictionary.TryGetValue(
                                                new Entry { Culture = strSourceCulture, Name = strName }, out oFoundEntry))
                                            {
                                                strLocalizedTextVariant = oFoundEntry.Value;
                                                if (!string.IsNullOrEmpty(strLocalizedTextVariant))
                                                    translations.Add(new Translation
                                                    {
                                                        Language = strSourceCulture,
                                                        Text = strLocalizedTextVariant
                                                    });
                                            };
                                        }



                                        string bestTranslation = GetBestTranslation(
                                            translations, strCulture, strAPIKey, strTranslationService);


                                        if (!string.IsNullOrEmpty(bestTranslation))
                                            outputlist.Add(new Entry
                                            {
                                                Culture = strCulture,
                                                Name = strName,
                                                Value = bestTranslation,
                                                Comment = "Generated by AI"
                                            });
                                    }
                                }
                            }
                    }
                    else
                    {
                        foreach (string strCulture in distinctCultures)
                            foreach (string strName in distinctNames)
                            {
                                Entry oEntry;
                                if (oEntriesDictionary.TryGetValue(
                                    new Entry { Culture = strCulture, Name = strName }, out oEntry))
                                {
                                    outputlist.Add(oEntry);
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(strAPIKey))
                                    {
                                        outputlist.Add(new Entry
                                        {
                                            Comment = "Empty space for " + strCulture + " - " + strName
                                        });
                                    }
                                    else
                                    {
                                        var translations = new List<Translation>();

                                        string strLocalizedTextVariant = null;
                                        Entry oFoundEntry = null;
                                        if (oEntriesDictionary.TryGetValue(
                                            new Entry { Culture = "(default)", Name = strName }, out oFoundEntry))
                                        {
                                            strLocalizedTextVariant = oFoundEntry.Value;
                                            if (!string.IsNullOrEmpty(strLocalizedTextVariant))
                                                translations.Add(new Translation
                                                    {
                                                        Language = "en",
                                                        Text = strLocalizedTextVariant
                                                    });
                                        };


                                        foreach (string strSourceCulture in new string[] { "es", "de", "pt", "it", "en", "fr" })
                                        {
                                            if (oEntriesDictionary.TryGetValue(
                                                new Entry { Culture = strSourceCulture, Name = strName }, out oFoundEntry))
                                            {
                                                strLocalizedTextVariant = oFoundEntry.Value;
                                                if (!string.IsNullOrEmpty(strLocalizedTextVariant))
                                                    translations.Add(new Translation
                                                      {
                                                          Language = strSourceCulture,
                                                          Text = strLocalizedTextVariant
                                                      });
                                            };
                                        }



                                        string bestTranslation = GetBestTranslation(
                                            translations, strCulture, strAPIKey, strTranslationService);

                                        if (!string.IsNullOrEmpty(bestTranslation))
                                        outputlist.Add(new Entry
                                        {
                                            Culture = strCulture,
                                            Name = strName,
                                            Value = bestTranslation,
                                            Comment = "Generated by AI"
                                        });
                                    }
                                }
                            }
                    }

                    // write the csv file for convenient editing in sheet calculation programs
                    WriteToCsv(outputlist, System.IO.Path.Combine(strDirectory, strBaseName + ".csv"));

                }

            }
            catch (Exception oEx)
            {
                // write error and exit
                System.Console.Error.WriteLine(oEx);
            }
        }


        //===================================================================================================
        /// <summary>
        /// Reads a resx file and converts its strings to entries
        /// </summary>
        /// <param name="strFilePath">Path to read</param>
        /// <param name="strCulture">Culure for the entries</param>
        /// <returns>An enumeration of strings in the resx</returns>
        //===================================================================================================
        static IEnumerable<Entry> ReadResxFile(
            string strFilePath,
            string strCulture)
        {
            var oXmlDoc = XDocument.Load(strFilePath);
            var iDataElements = oXmlDoc.Root.Elements("data");

            foreach (var oElement in iDataElements)
            {
                XAttribute oNameAttribute = oElement.Attribute("name");
                var strName = oNameAttribute != null ? oNameAttribute.Value : null;
                var oValueNode = oElement.Element("value");
                var oValueTextNode = oValueNode != null ? oValueNode.FirstNode as XText : null;
                var strValue = oValueTextNode != null ? oValueTextNode.Value : null;
                var oCommentNode = oElement.Element("comment");
                var oCommentTextNode = oCommentNode != null ? oCommentNode.FirstNode as XText : null;
                var strComment = oCommentTextNode != null ? oCommentTextNode.Value : null;

                yield return new Entry
                {
                    Name = strName,
                    Value = strValue,
                    Comment = strComment,
                    Culture = strCulture
                };
            }
        }

        //===================================================================================================
        /// <summary>
        /// Writes Entries to a CSV file
        /// </summary>
        /// <param name="iEntries">An enumeration of entries</param>
        /// <param name="strFilePath">Path to write to</param>
        //===================================================================================================
        static void WriteToCsv(
            IEnumerable<Entry> iEntries,
            string strFilePath)
        {
            using (System.IO.StreamWriter oWriter =
                new StreamWriter(strFilePath, false, Encoding.UTF8))
            {
                oWriter.WriteLine("Culture;Name;Value;Comment");

                foreach (Entry oEntry in iEntries)
                {
                    oWriter.WriteLine(
                        ToCsv(oEntry.Culture) + ";" +
                        ToCsv(oEntry.Name) + ";" +
                        ToCsv(oEntry.Value) + ";" +
                        ToCsv(oEntry.Comment));
                }

                oWriter.Flush();
                oWriter.Close();
            }
        }

        //===================================================================================================
        /// <summary>
        /// Transforms a value to CSV format
        /// </summary>
        /// <param name="strValue">Value to transform</param>
        /// <returns>Value in CSV format</returns>
        //===================================================================================================
        static string ToCsv(string strValue)
        {
            if (strValue == null)
                return "";

            if (strValue.Contains("\"") || strValue.Contains(";") || strValue.Contains("\n"))
                return "\"" + (strValue.Replace("\"", "\"\"")) + "\"";
            else
                return strValue;
        }


        //===================================================================================================
        /// <summary>
        /// Reads a CSV file and provides its entries
        /// </summary>
        /// <param name="strFilePath">Path to load</param>
        /// <returns>Loaded entries</returns>
        //===================================================================================================
        static IEnumerable<Entry> ReadCsvFile(
            string strFilePath
            )
        {
            using (StreamReader oReader = new StreamReader(strFilePath))
            {
                bool bHeaders = true;
                string strLine;
                string strPartialLine = null;
                Dictionary<eColumn, int> oColumnHeadersDictionary = new Dictionary<eColumn, int>();

                while ((strLine = oReader.ReadLine()) != null)
                {
                    if (strPartialLine != null)
                    {
                        strLine = strPartialLine + Environment.NewLine + strLine;
                        strPartialLine = null;
                    }

                    var astrValues = ParseCsvLine(strLine);

                    if (IsMultiline(astrValues))
                    {
                        strPartialLine = strLine;
                        continue;
                    }

                    if (bHeaders)
                    {
                        for (int i = 0; i < astrValues.Length; ++i)
                        {
                            switch (astrValues[i])
                            {
                                case "Culture":
                                    oColumnHeadersDictionary[eColumn.Culture] = i;
                                    break;
                                case "Name":
                                    oColumnHeadersDictionary[eColumn.Name] = i;
                                    break;
                                case "Value":
                                    oColumnHeadersDictionary[eColumn.Value] = i;
                                    break;
                                case "Comment":
                                    oColumnHeadersDictionary[eColumn.Comment] = i;
                                    break;
                            }
                        }
                        bHeaders = false;
                    }
                    else
                    {
                        yield return new Entry
                        {
                            Culture = oColumnHeadersDictionary.ContainsKey(eColumn.Culture) ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Culture]] :
                                    null,
                            Name = oColumnHeadersDictionary.ContainsKey(eColumn.Name) ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Name]] :
                                    null,
                            Value = oColumnHeadersDictionary.ContainsKey(eColumn.Value) ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Value]] :
                                    null,
                            Comment = oColumnHeadersDictionary.ContainsKey(eColumn.Comment) ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Comment]] :
                                    null
                        };
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Provides information, if the values haven't been finished in line and a next line needs to be read
        /// </summary>
        /// <param name="astrValues">Read values</param>
        /// <returns>true iff there is a not closed doouble quote</returns>
        //===================================================================================================
        static bool IsMultiline(
            string[] astrValues
            )
        {
            foreach (var value in astrValues)
            {
                if (value.StartsWith("\"") && !value.EndsWith("\""))
                {
                    return true;
                }
            }
            return false;
        }

        //===================================================================================================
        /// <summary>
        /// Parses a CSV line and splits it into separate values
        /// </summary>
        /// <param name="strLine">Line to split</param>
        /// <returns>Split line</returns>
        //===================================================================================================
        static string[] ParseCsvLine(
            string strLine
            )
        {
            var oMatches = Regex.Matches(strLine,
                @"(?<=^|;)(?:""(?<value>(?:[^""]|"""")*)""|(?<value>[^;]*))",
                RegexOptions.Compiled);

            return oMatches.Cast<Match>().Select(m => m.Groups["value"].Value.Replace("\"\"", "\"")).ToArray();
        }

        //===================================================================================================
        /// <summary>
        /// Writes entries to a resx file, completely overwriting itt
        /// </summary>
        /// <param name="iEntries">Entries to write</param>
        /// <param name="strFilePath">Path to write to</param>
        //===================================================================================================
        static void WriteToResx(
            IEnumerable<Entry> iEntries,
            string strFilePath
            )
        {
            var doc = new XDocument(
                new XElement("root",
                    iEntries.Cast<Entry>().Select(row =>
                        new XElement("data",
                            new XAttribute("name", row.Name),
                            new XElement("value", row.Value)
                        )
                    )
                )
            );

            doc.Save(strFilePath);
        }

        //===================================================================================================
        /// <summary>
        /// Updates entries in a resx file
        /// </summary>
        /// <param name="iNewValues">New values</param>
        /// <param name="strFilePath">Path to update</param>
        //===================================================================================================
        public static void UpdateResxFile(
            IEnumerable<Entry> iNewValues,
            string strFilePath
            )
        {

            var oXmlDoc = XDocument.Load(strFilePath);

            foreach (var oNewValue in iNewValues)
            {
                if (string.IsNullOrEmpty(oNewValue.Name))
                    continue;

                var oElement = oXmlDoc.Root.Elements("data")
                    .FirstOrDefault(e => e.Attribute("name").Value == oNewValue.Name);

                if (oElement != null)
                {
                    var oValueElement = oElement.Element("value");
                    oValueElement.Value = oNewValue.Value;
                    //valueElement.SetAttributeValue(XNamespace.Xml + "space", "preserve");

                    var oCommentElement = oElement.Element("comment");
                    if (oCommentElement != null)
                    {
                        oCommentElement.Value = oNewValue.Comment;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(oNewValue.Comment) &&
                            !oNewValue.Comment.StartsWith("Empty space for "))
                        {
                            oElement.Add(new XElement("comment", oNewValue.Comment));
                        }
                    }
                }
                else
                {
                    var oNewElement = new XElement("data",
                        new XAttribute(XNamespace.Xml + "space", "preserve"),
                        new XAttribute("name", oNewValue.Name),
                        new XElement("value", oNewValue.Value
                        ));

                    if (!string.IsNullOrEmpty(oNewValue.Comment) &&
                        !oNewValue.Comment.StartsWith("Empty space for "))
                    {
                        oNewElement.Add(new XElement("comment", oNewValue.Comment));
                    }

                    oXmlDoc.Root.Add(oNewElement);
                }
            }

            oXmlDoc.Save(strFilePath);

        }



        //===================================================================================================
        /// <summary>
        /// Translates a text with specified engine
        /// </summary>
        /// <param name="strSourceLanguage">Source language to translate from</param>
        /// <param name="strText">Text to translate</param>
        /// <param name="strTargetLanguage">Target language</param>
        /// <param name="strAPIKey">API Key</param>
        /// <param name="strService">Service to use, either microsoft or google</param>
        /// <returns>Translated string</returns>
        //===================================================================================================
        public static string Translate(
            string strSourceLanguage, 
            string strText, 
            string strTargetLanguage, 
            string strAPIKey, 
            string strService)
        {
            switch (strService.ToLower())
            {
                case "google":
                    return TranslateWithGoogle(strText, strSourceLanguage, strTargetLanguage, strAPIKey);
                case "microsoft":
                    return TranslateWithMicrosoft(strText, strSourceLanguage, strTargetLanguage, strAPIKey);
                default:
                    throw new ArgumentException("Unknown translation service specified.");
            }
        }
        

        //===================================================================================================
        /// <summary>
        /// Translates a string with google engine
        /// </summary>
        /// <param name="strText">Text to translate</param>
        /// <param name="strSourceLanguage">Source language</param>
        /// <param name="strTargetLanguage">Target language</param>
        /// <param name="strAPIKey">Key</param>
        /// <returns>Translated string</returns>
        //===================================================================================================
        private static string TranslateWithGoogle(
            string strText, 
            string strSourceLanguage, 
            string strTargetLanguage, 
            string strAPIKey)
        {
            string url = @"https://translation.googleapis.com/language/translate/v2?key=" +
                strAPIKey + "&q=" + Uri.EscapeDataString(strText) + "&source=" + strSourceLanguage + 
                "&target=" + strTargetLanguage;

            using (WebClient oWebClient = new WebClient())
            {
                oWebClient.Encoding = Encoding.UTF8;
                string oResponse = oWebClient.DownloadString(url);
                return ExtractTranslatedTextFromGoogleResponse(oResponse);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Translates a string with microsoft engine
        /// </summary>
        /// <param name="strText">Text to translate</param>
        /// <param name="strSourceLanguage">Source language</param>
        /// <param name="strTargetLanguage">Target language</param>
        /// <param name="strAPIKey">Key</param>
        /// <returns>Translated string</returns>
        //===================================================================================================
        private static string TranslateWithMicrosoft(
            string strText, 
            string strSourceLanguage,
            string strTargetLanguage,
            string strAPIKey)
        {
            string url = @"https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from=" + 
                strSourceLanguage + "&to=" + strTargetLanguage;

            using (WebClient oWebClient = new WebClient())
            {
                oWebClient.Encoding = Encoding.UTF8;
                oWebClient.Headers.Add("Ocp-Apim-Subscription-Key", strAPIKey);
                oWebClient.Headers.Add("Content-Type", "application/json");

                string strBody = "[{\"Text\":\"" + strText + "\"}]";
                string strResponse = oWebClient.UploadString(url, strBody);
                return ExtractTranslatedTextFromMicrosoftResponse(strResponse);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Extracts result from response
        /// </summary>
        /// <param name="strResponse">Response from google service</param>
        /// <returns>The translated text from response</returns>
        //===================================================================================================
        private static string ExtractTranslatedTextFromGoogleResponse(
            string strResponse
            )
        {
            // Basic string manipulation to extract the translated text
            int nStartIndex = strResponse.IndexOf("\"translatedText\":\"") + 18;
            int nEndIndex = strResponse.IndexOf("\"", nStartIndex);
            return strResponse.Substring(nStartIndex, nEndIndex - nStartIndex);
        }

        //===================================================================================================
        /// <summary>
        /// Extracts result from response
        /// </summary>
        /// <param name="strResponse">Response from microsoft service</param>
        /// <returns>The translated text from response</returns>
        //===================================================================================================
        private static string ExtractTranslatedTextFromMicrosoftResponse(string strResponse)
        {
            // Basic string manipulation to extract the translated text
            int startIndex = strResponse.IndexOf("\"text\":\"") + 8;
            int endIndex = strResponse.IndexOf("\"", startIndex);
            return strResponse.Substring(startIndex, endIndex - startIndex);
        }

        //===================================================================================================
        /// <summary>
        /// Extracts translated text from json result
        /// </summary>
        /// <param name="strJsonResponse">JSON response</param>
        /// <returns>Translation</returns>
        //===================================================================================================
        private static string ExtractTranslatedText(
            string strJsonResponse
            )
        {
            // Basic string manipulation to extract the translated text
            int nStartIndex = strJsonResponse.IndexOf("[[[") + 4;
            int nEndIndex = strJsonResponse.IndexOf("\",\"", nStartIndex);
            return strJsonResponse.Substring(nStartIndex, nEndIndex - nStartIndex);
        }


        //===================================================================================================
        /// <summary>
        /// Gets hopefully the best translation, based on a set of texts in different languages
        /// </summary>
        /// <param name="iTranslations">The translations of the text in different languages</param>
        /// <param name="strTargetLanguage">The target language</param>
        /// <param name="strAPIKey">API Key for querying translations</param>
        /// <returns>The hopefully one and only translation</returns>
        //===================================================================================================
        public static string GetBestTranslation(
            IEnumerable<Translation> iTranslations,
            string strTargetLanguage,
            string strAPIKey,
            string strService)
        {
            List<string> oTranslationVariantsToTargetLanguage = new List<string>();
            foreach (Translation oTranslation in iTranslations)
            {
                string strResult = Translate(oTranslation.Language, oTranslation.Text, strTargetLanguage, 
                    strAPIKey, strService);
                if (!string.IsNullOrEmpty(strResult))
                {
                    oTranslationVariantsToTargetLanguage.Add(strResult);
                }
            }

            // Find the translation with the smallest edit distance
            string strBestTranslation = null;
            int nMinEditDistance = int.MaxValue;

            foreach (string strVariant in oTranslationVariantsToTargetLanguage)
            {
                int nSum = 0;
                foreach (string strOtherVariant in oTranslationVariantsToTargetLanguage)
                {
                    nSum = nSum + GetEditDistance(strVariant, strOtherVariant);
                }

                if (nSum < nMinEditDistance)
                {
                    nMinEditDistance = nSum;
                    strBestTranslation = strVariant;
                }
            }

            return strBestTranslation;
        }

        //===================================================================================================
        /// <summary>
        /// Calculate the edit distance between two strings
        /// </summary>
        /// <param name="str1">first string</param>
        /// <param name="str2">second string</param>
        /// <returns>The edit distance</returns>
        //===================================================================================================
        private static int GetEditDistance(string str1, string str2)
        {
            int[,] aDist = new int[str1.Length + 1, str2.Length + 1];

            for (int i = 0; i <= str1.Length; i++)
                aDist[i, 0] = i;
            for (int j = 0; j <= str2.Length; j++)
                aDist[0, j] = j;

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    int nCost = str1[i - 1] == str2[j - 1] ? 0 : 1;
                    aDist[i, j] = Math.Min(Math.Min(aDist[i - 1, j] + 1, aDist[i, j - 1] + 1), aDist[i - 1, j - 1] + nCost);
                }
            }

            return aDist[str1.Length, str2.Length];

        }
    }
}
