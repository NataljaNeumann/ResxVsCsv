/* MIT License

Copyright (c) 2025 NataljaNeumann@gmx.de

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
#if DEBUG
//#define TEST_TRANSLATION_LOGIC
#endif

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using System.Runtime.InteropServices;
using System.Text.Json;


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
            Comment,
            Type,
            MimeType
        }

        //===================================================================================================
        /// <summary>
        /// Special comment that indicates that the value is missing (not empty)
        /// </summary>
        const string c_strSpecialComment = "Please fill in the needed translation";

#if TEST_TRANSLATION_LOGIC
        //===================================================================================================
        /// <summary>
        /// Correct translations, just for testing
        /// </summary>
        static List<Translation> m_oTranslations;
#endif

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
#if DEBUG
            //set test culture
            //string strSetCulture =
                // "af-ZA";
                // "ar-SA";
                // "az-Latn-AZ";
                // "be-BY";
                // "bg-BG";
                // "bs-Latn-BA";
                // "cs-CZ";
                // "da-DK";
                // "de-DE";
                // "el-GR";
                // "es-ES";
                // "et-EE";
                // "fa-IR";
                // "fi-FI";
                // "fr-FR";
                // "he-IL";
                // "hi-IN";
                // "hu-HU";
                // "hy-AM";
                // "id-ID";
                // "is-IS";
                // "it-IT";
                // "ja-JP";
                // "ka-GE";
                // "kk-KZ";
                // "km-KH";
                // "ko-KR";
                // "ky-KG";
                // "lt-LT";
                // "lv-LV";
                // "mk-MK";
                // "mn-MN";
                // "ms-MY";
                // "nl-NL";
                // "no-NO";
                // "pa-Arab-PK";
                // "pa-IN";
                // "pl-PL";
                // "ps-AF";
                // "pt-PT";
                // "en-US";
                // "ro-RO";
                // "ru-RU";
                // "sa-IN";
                // "sk-SK";
                // "sl-SL";
                // "sr-Latn-RS"; // TODO: need a fix
                // "sv-SE";
                // "tg-Cyrl-TJ";
                // "th-TH";
                // "tr-TR";
                // "uk-UA";
                // "uz-Latn-UZ";
                // "vi-VN";
                // "zh-TW";
                // "zh-CN";
            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(strSetCulture);
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(strSetCulture);
#endif

            try
            {
                string strPattern = "Resources.*resx";
                string strDirectory = Directory.GetCurrentDirectory();
                string? strToResx = null;
                bool bSortByName = false;
                string? strApiKey = null;
                string? strApiUrl = null;
                string? strTranslationService = null;
                string? strAddCultures = null;
                string? strLLMModel = "";
                bool bOnlyStrings = true;
                bool bRemoveDuplicates = false;
                bool bFixFonts = false;
                bool bBruteForce = false;
                string strDefaultCulture = "en";
                if (aArgs.Length == 0)
                    aArgs = new string[] { "/?" };


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
                                bSortByName = "yes".Equals(aArgs[++i]);
                            }
                            break;
                        case "--removeduplicates":
                            if (i + 1 < aArgs.Length)
                            {
                                bRemoveDuplicates = "yes".Equals(aArgs[++i]);
                            }
                            break;
                        case "--onlystrings":
                            if (i + 1 < aArgs.Length)
                            {
                                bOnlyStrings = "yes".Equals(aArgs[++i]);
                            }
                            break;
                        case "--addcultures":
                            if (i + 1 < aArgs.Length)
                            {
                                strAddCultures = aArgs[++i];
                            }
                            break;
                        case "--libreurl":
                        case "--llmurl":
                            if (i + 1 < aArgs.Length)
                            {
                                strApiUrl = aArgs[++i];
                            }
                            break;

                        case "--apikey":
                            if (i + 1 < aArgs.Length)
                            {
                                strApiKey = aArgs[++i];
                            }
                            break;
                        case "--fixfonts":
                            if (i + 1 < aArgs.Length)
                            {
                                bFixFonts = "yes".Equals(aArgs[++i]);
                            }
                            break;
                        case "--bruteforce":
                            if (i + 1 < aArgs.Length)
                            {
                                bBruteForce = "yes".Equals(aArgs[++i]);
                            }
                            break;
                        case "--defaultculture":
                            if (i + 1 < aArgs.Length)
                            {
                                strDefaultCulture = aArgs[++i];
                                if (string.IsNullOrEmpty(strDefaultCulture))
                                {
                                    strDefaultCulture = "en";
                                }
                            }
                            break;
                        case "--llmmodel":
                            if (i + 1 < aArgs.Length)
                            {
                                strLLMModel = aArgs[++i];
                            }
                            break;
                        case "--help":
                        case "-?":
                        case "/?":
                            try
                            {
                                string strUrl = System.IO.Path.Combine(
                                    AppDomain.CurrentDomain.BaseDirectory,
                                    "Readme.html");

                                try
                                {
                                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                                    {
                                        Process.Start(new ProcessStartInfo(strUrl) { UseShellExecute = true });
                                    }
                                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                                    {
                                        Process.Start("xdg-open", strUrl);
                                    }
                                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                                    {
                                        Process.Start("open", strUrl);
                                    }
                                }
                                catch (Exception oEx)
                                {
                                    System.Console.Error.WriteLine("Could not open browser: " + oEx.Message);
                                }

                            }
                            catch
                            {
                                // ignore
                            }
                            goto case "--licence";
                        case "--licence":
                        case "--license":
                            Version? oVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                            WriteWrappedText(
                                string.Format("ResxVsCsv v{0}.{1} {2} {3}",
                                    oVersion?.Major, oVersion?.Minor,
                                    Properties.Resources.CopyrightMessage, "NataljaNeumann@gmx.de"));

                            System.Console.WriteLine();

                            WriteWrappedText(Properties.Resources.LicenseText);

                            System.Console.WriteLine();

                            WriteWrappedText(
                                Properties.Resources.ForConversionToCsv +
                                "ResxVsCsv --directory <dir> --pattern <pattern> [--sortbyname yes] [--onlystrings no]\r\n"+
                                "  [--addcultures <comma-separated-list>]");

                            WriteWrappedText(
                                Properties.Resources.ForTranslation +
                                "ResxVsCsv --directory <dir> --pattern <pattern> \r\n" +
                                "  --translator <google|microsoft|deepl|toptranslation> --apikey <key> [--sortbyname yes]\r\n" +
                                "  [--bruteforce yes] [--defaultculture <culture>]");

                            WriteWrappedText(
                                Properties.Resources.ForTranslationWithArgos +
                                "ResxVsCsv --directory <dir> --pattern <pattern> \r\n" +
                                "  --translator argos [--sortbyname yes]\r\n" +
                                "  [--bruteforce yes] [--defaultculture <culture>]");

                            WriteWrappedText(
                                Properties.Resources.ForTranslationWithLibreTranslate +
                                "ResxVsCsv --directory <dir> --pattern <pattern> \r\n" +
                                "  --translator libretranslate --libreurl <url> [--apikey <key>] [--sortbyname yes]\r\n" +
                                "  [--bruteforce yes] [--defaultculture <culture>]");

                            WriteWrappedText(
                                Properties.Resources.ForTranslationWithLLM +
                                "ResxVsCsv --directory <dir> --pattern <pattern> \r\n" +
                                "  --translator llm --llmeurl <url> --llmmodel <moel> [--apikey <key>] [--sortbyname yes]\r\n" +
                                "  [--bruteforce yes] [--defaultculture <culture>]");


                            WriteWrappedText(
                                Properties.Resources.ForUpdatingResxFiles +
                                "ResxVsCsv --directory <dir> --toresx <resources.csv> [--removeduplicates yes]\r\n"+
                                "  [--addcultures <comma-separated-list>] [--fixfonts yes]");

                            return;
                    }
                }

                if (strToResx != null)
                {
                    // the transformation is back from CSV to RESX

                    // read the CSV file
                    List<Entry> oEntries = new List<Entry>(
                        ReadCsvFile(System.IO.Path.Combine(strDirectory, strToResx)));

                    if (oEntries.Count == 0)
                    {
                        WriteWrappedText(
                            string.Format(Properties.Resources.WarningNoEntriesInFile,
                            System.IO.Path.Combine(strDirectory, strToResx)));
                    }
                    else
                    {

                        Dictionary<string, string> oFonts = new Dictionary<string, string>();

                        if (bFixFonts)
                        {
                            foreach (Entry oFontEntry in
                                oEntries
                                .Where(x => !string.IsNullOrEmpty(x.Type) &&
                                    !string.IsNullOrEmpty(x.Value) &&
                                    "System.Drawing.Font, System.Drawing".Equals(x.Type)))
                            {
                                // actually already tested above, but compiler wants it.
                                if (oFontEntry.Value != null)
                                {
                                    oFonts[oFontEntry.Culture + "_" + oFontEntry.Name] = oFontEntry.Value;
                                }
                            }
                        }

                        #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                        List<string> aDistinctCultures = oEntries
                            .Where(x => !string.IsNullOrEmpty(x.Culture) &&
                                        !string.IsNullOrEmpty(x.Name) &&
                                        (!string.IsNullOrEmpty(x.Value) ||
                                            (!string.IsNullOrEmpty(x.Comment) &&
                                                !x.Comment.Equals(c_strSpecialComment))))
                            .Select(x => x.Culture)
                            .Distinct()
                            .ToList();
                        #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.


                        if (!string.IsNullOrEmpty(strAddCultures))
                            AddCulturesIfMissing(aDistinctCultures, 
                                strAddCultures.Split(new char [] {','}, StringSplitOptions.RemoveEmptyEntries));

                        if (aDistinctCultures.Count == 0)
                        {
                            WriteWrappedText(
                                string.Format(Properties.Resources.WarningLoadingEntriesFromFileFailed,
                                    System.IO.Path.Combine(strDirectory, strToResx)));
                        }

                        List<string> aResxFiles = new List<string>();
                        string strDefaultCulturePath = System.IO.Path.Combine(strDirectory, strToResx.Replace(".csv", ".resx"));

                        // update single resx files
                        foreach (string strCulture in aDistinctCultures)
                        {
                            string strResxCulture = strCulture.Equals("(default)") ? "" : "." + strCulture;
                            string strResxFile = System.IO.Path.Combine(strDirectory, strToResx.Replace(".csv", strResxCulture + ".resx"));
                            aResxFiles.Add(strResxFile);
                            UpdateResxFile(oEntries.Where(x => x.Culture != null && x.Culture.Equals(strCulture) && !string.IsNullOrEmpty(x.Value)),
                                strResxFile, strDefaultCulturePath, oFonts);
                        }

                        // if we need to remove duplicate entries then do an addiional step
                        if (bRemoveDuplicates)
                            RemoveDuplicateDataElements(strDefaultCulturePath, aResxFiles);
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

                    List<string> astrDistinctCultures = new List<string>();

                    // Print selected files
                    foreach (var strFilePath in aFiles)
                    {
                        // calc components of the file name
                        string strFileName = strFilePath.Substring(strFilePath.LastIndexOf('\\') + 1);
                        string strFileNameWithoutExt = strFileName.Substring(0, strFileName.LastIndexOf('.'));
                        string strCulture = strFileNameWithoutExt.Contains('.') ?
                            strFileNameWithoutExt.Substring(strFileNameWithoutExt.LastIndexOf('.') + 1) :
                            "(default)";
                        astrDistinctCultures.Add(strCulture);
                        strBaseName = strFileNameWithoutExt.Contains('.') ?
                            strFileNameWithoutExt.Substring(0, strFileNameWithoutExt.LastIndexOf('.')) :
                            strFileNameWithoutExt;
                        // read the resx file and add entries to the overall list
                        oAllEntries.AddRange(ReadResxFile(strFilePath, strCulture, bOnlyStrings));
                    }

                    if (!string.IsNullOrEmpty(strAddCultures))
                        AddCulturesIfMissing(astrDistinctCultures, 
                            strAddCultures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                    // find distinct cultures
                    /*
                    var distinctCultures = oAllEntries
                        .Where(x => !string.IsNullOrEmpty(x.Culture))
                        .Select(x => x.Culture)
                        .Distinct()
                        .ToList();
                     */

                    #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                    // find distinct names for values
                    List<string> astrDistinctNames = oAllEntries
                        .Where(x => !string.IsNullOrEmpty(x.Name))
                        .Select(x => x.Name)
                        .Distinct()
                        .ToList();
                    #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

                    // Dictionary with types
                    Dictionary<string, string> oNameTypes = new Dictionary<string, string>();
                    foreach (Entry oEntry in oAllEntries)
                        if (!string.IsNullOrEmpty(oEntry.Name) && !string.IsNullOrEmpty(oEntry.Type))
                            oNameTypes[oEntry.Name] = oEntry.Type;

                    Dictionary<string, string> oNameMimeTypes = new Dictionary<string, string>();
                    foreach (Entry oEntry in oAllEntries)
                        if (!string.IsNullOrEmpty(oEntry.Name) && !string.IsNullOrEmpty(oEntry.MimeType))
                            oNameMimeTypes[oEntry.Name] = oEntry.MimeType;

                    // collect all entries in a single dictionary for fast access
                    Dictionary<Entry, Entry> oEntriesDictionary = new Dictionary<Entry, Entry>();

                    foreach (Entry oEntry in oAllEntries)
                        oEntriesDictionary[oEntry] = oEntry;

                    // Now reorder the elements in needed order for output
                    List<Entry> aOutputList = new List<Entry>();

                    if (bSortByName)
                    {
                        foreach (string strName in astrDistinctNames)
                            foreach (string strCulture in astrDistinctCultures)
                            {
                                string strComment = "";

                                Entry? oEntry;
#if TEST_TRANSLATION_LOGIC
                                if ((string.IsNullOrEmpty(strTranslationService) || 
                                     oNameTypes.ContainsKey(strName) ||
                                     strName.StartsWith(">>")) &&
                                     oEntriesDictionary.TryGetValue(
                                        new Entry { Culture = strCulture, Name = strName }, out oEntry))
#else
                                if (oEntriesDictionary.TryGetValue(
                                    new Entry { Culture = strCulture, Name = strName }, out oEntry))
#endif
                                {
                                    aOutputList.Add(oEntry);

                                    if ((string.IsNullOrEmpty(strComment) &&
                                        !string.IsNullOrEmpty(oEntry.Comment)) ||
                                        "(default)".Equals(oEntry.Culture))
                                    {
                                        strComment = oEntry.Comment;
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(strTranslationService) || 
                                        oNameTypes.ContainsKey(strName) || 
                                        strName.StartsWith(">>"))
                                    {
                                        aOutputList.Add(new Entry
                                        {
                                            Culture = strCulture,
                                            Name = strName,
                                            Comment = oNameTypes.ContainsKey(strName) || strName.StartsWith(">>") ? "" : c_strSpecialComment,
                                            Type = oNameTypes.ContainsKey(strName) ? oNameTypes[strName] : null,
                                            MimeType = oNameMimeTypes.ContainsKey(strName) ? oNameMimeTypes[strName] : null,
                                        });
                                    }
                                    else
                                    {
                                        try
                                        {
                                            List<Translation> oTranslations = new List<Translation>();
#if TEST_TRANSLATION_LOGIC
                                            m_oTranslations = oTranslations;
#endif


                                            if (!bBruteForce)
                                            {
                                                // without brute force we take only the big languages
                                                string? strLocalizedTextVariant = null;
                                                Entry? oFoundEntry = null;

                                                foreach (string strSourceCulture in new string[] { 
                                                        "es", "de", "pt", "it", "fr", "ru", "ko", "ja", "zh-CHS", "zh-CHT", "hi", "en" })
                                                {
                                                    if (oEntriesDictionary.TryGetValue(
                                                        new Entry { Culture = strSourceCulture, Name = strName }, out oFoundEntry))
                                                    {
                                                        strLocalizedTextVariant = oFoundEntry.Value;
                                                        if (!string.IsNullOrEmpty(strLocalizedTextVariant))
                                                            oTranslations.Add(new Translation
                                                            {
                                                                Language = strSourceCulture,
                                                                Text = strLocalizedTextVariant
                                                            });
                                                    };
                                                }

                                                if (oEntriesDictionary.TryGetValue(
                                                    new Entry { Culture = "(default)", Name = strName }, out oFoundEntry))
                                                {
                                                    strLocalizedTextVariant = oFoundEntry.Value;
                                                    if (!string.IsNullOrEmpty(strLocalizedTextVariant))
                                                        oTranslations.Add(new Translation
                                                        {
                                                            Language = strDefaultCulture,
                                                            Text = strLocalizedTextVariant
                                                        });
                                                };

                                            }
                                            else
                                            {
                                                // in brute force mode we get all available variants
                                                foreach (Entry oEntry2 in oAllEntries)
                                                {
                                                    if (!string.IsNullOrEmpty(oEntry2.Name) &&
                                                        oEntry2.Name.Equals(strName) &&
                                                        !string.IsNullOrEmpty(oEntry2.Value))
                                                    {
                                                        string? strSourceCulture = oEntry2.Culture;
                                                        if (!string.IsNullOrEmpty(strSourceCulture) &&
                                                            strSourceCulture.Equals("(default)"))
                                                        {
                                                            strSourceCulture = strDefaultCulture;
                                                        }

                                                        oTranslations.Add(new Translation
                                                        {
                                                            Language = strSourceCulture,
                                                            Text = oEntry2.Value
                                                        });
                                                    }
                                                }
                                            }



                                            string? strBestTranslation = GetBestTranslation(
                                                oTranslations, strCulture, strApiKey, strTranslationService, 
                                                strApiUrl, bBruteForce, strLLMModel, strName, strComment);


                                            if (!string.IsNullOrEmpty(strBestTranslation))
                                                aOutputList.Add(new Entry
                                                {
                                                    Culture = strCulture,
                                                    Name = strName,
                                                    Value = strBestTranslation,
                                                    Comment = Properties.Resources.GeneratedByAi
                                                });

                                            WriteWrappedText(
                                                string.Format(Properties.Resources.TranslatedCultureString,
                                                    strCulture + " - " + strName));
                                        }
                                        catch (Exception oEx)
                                        {
                                            System.Console.Error.WriteLine(
                                                string.Format(Properties.Resources.ErrorWhileTranslating,
                                                strCulture + " - " + strName + ": " + oEx.Message));
                                        }
                                    }
                                }
                            }
                    }
                    else
                    {
                        foreach (string strCulture in astrDistinctCultures)
                            foreach (string strName in astrDistinctNames)
                            {
                                Entry? oEntry;
                                string strComment = "";
#if TEST_TRANSLATION_LOGIC
                                if ((string.IsNullOrEmpty(strTranslationService) ||
                                     oNameTypes.ContainsKey(strName) ||
                                     strName.StartsWith(">>")) &&
                                     oEntriesDictionary.TryGetValue(
                                        new Entry { Culture = strCulture, Name = strName }, out oEntry))
#else
                                if (oEntriesDictionary.TryGetValue(
                                    new Entry { Culture = strCulture, Name = strName }, out oEntry))
#endif
                                {
                                    aOutputList.Add(oEntry);

                                    if ((string.IsNullOrEmpty(strComment) &&
                                        !string.IsNullOrEmpty(oEntry.Comment)) ||
                                        "(default)".Equals(oEntry.Culture))
                                    {
                                        strComment = oEntry.Comment;
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(strTranslationService) || oNameTypes.ContainsKey(strName) || strName.StartsWith(">>"))
                                    {
                                        aOutputList.Add(new Entry
                                        {
                                            Culture = strCulture,
                                            Name = strName,
                                            Comment = oNameTypes.ContainsKey(strName) || strName.StartsWith(">>") ? "" : c_strSpecialComment,
                                            Type = oNameTypes.ContainsKey(strName) ? oNameTypes[strName] : null,
                                            MimeType = oNameMimeTypes.ContainsKey(strName) ? oNameMimeTypes[strName] : null

                                        });
                                    }
                                    else
                                    {
                                        try
                                        {
                                            List<Translation> oTranslations = new List<Translation>();
                                            
#if TEST_TRANSLATION_LOGIC
                                            m_oTranslations = oTranslations;
#endif

                                            if (!bBruteForce)
                                            {
                                                // without brute force we take only the big languages
                                                string? strLocalizedTextVariant = null;
                                                Entry? oFoundEntry = null;

                                                foreach (string strSourceCulture in new string[] { 
                                                            "es", "de", "pt", "it", "fr", "ru", "ko", "ja", "zh-CHS", "zh-CHT", "hi", "en" })
                                                {
                                                    if (oEntriesDictionary.TryGetValue(
                                                        new Entry { Culture = strSourceCulture, Name = strName }, out oFoundEntry))
                                                    {
                                                        strLocalizedTextVariant = oFoundEntry.Value;

                                                        if (!string.IsNullOrEmpty(strLocalizedTextVariant))
                                                            oTranslations.Add(new Translation
                                                              {
                                                                  Language = strSourceCulture,
                                                                  Text = strLocalizedTextVariant
                                                              });

                                                    }
                                                }

                                                if (oEntriesDictionary.TryGetValue(
                                                    new Entry { Culture = "(default)", Name = strName }, out oFoundEntry))
                                                {
                                                    strLocalizedTextVariant = oFoundEntry.Value;
                                                    if (!string.IsNullOrEmpty(strLocalizedTextVariant))
                                                        oTranslations.Add(new Translation
                                                        {
                                                            Language = strDefaultCulture,
                                                            Text = strLocalizedTextVariant
                                                        });
                                                };
                                            }
                                            else
                                            {
                                                // in brute force mode we get all available variants
                                                foreach (Entry oEntry2 in oAllEntries)
                                                {
                                                    if (!string.IsNullOrEmpty(oEntry2.Name) &&
                                                        oEntry2.Name.Equals(strName) &&
                                                        !string.IsNullOrEmpty(oEntry2.Value))
                                                    {
                                                        string? strSourceCulture = oEntry2.Culture;
                                                        if (!string.IsNullOrEmpty(strSourceCulture) &&
                                                            strSourceCulture.Equals("(default)"))
                                                        {
                                                            strSourceCulture = strDefaultCulture;
                                                        }

                                                        oTranslations.Add(new Translation
                                                        {
                                                            Language = strSourceCulture,
                                                            Text = oEntry2.Value
                                                        });
                                                    }
                                                }
                                            }



                                            string? strBestTranslation = GetBestTranslation(
                                                oTranslations, strCulture, strApiKey, strTranslationService, 
                                                strApiUrl, bBruteForce, strLLMModel, strName, strComment);

                                            if (!string.IsNullOrEmpty(strBestTranslation))
                                                aOutputList.Add(new Entry
                                                {
                                                    Culture = strCulture,
                                                    Name = strName,
                                                    Value = strBestTranslation,
                                                    Comment = Properties.Resources.GeneratedByAi
                                                });

                                            WriteWrappedText(
                                                string.Format(Properties.Resources.TranslatedCultureString,
                                                    strCulture + " - " + strName));
                                        }
                                        catch (Exception oEx)
                                        {
                                            System.Console.Error.WriteLine(
                                                string.Format(
                                                    Properties.Resources.ErrorWhileTranslating,
                                                    strCulture + " - " + strName + ": " + oEx.Message));
                                        }
                                    }
                                }
                            }
                    }

                    // write the csv file for convenient editing in sheet calculation programs
                    WriteToCsv(aOutputList, System.IO.Path.Combine(strDirectory, strBaseName + ".csv"));
                }

            }
            catch (Exception oEx)
            {
                // write error and exit
                WriteWrappedText(oEx.Message);
            }
        }


        //===================================================================================================
        /// <summary>
        /// Writes wrapped text to console
        /// </summary>
        /// <param name="strText">Text to write</param>
        /// <param name="nWindowWidth">Window width</param>
        /// <param name="bRightAligned">Indicates, if the text shall appear right-aligned</param>
        //===================================================================================================
        static void WriteWrappedText(
            string strText
            )
        {
            // for some languages we don't wrap the text, because it is unclear where the words start and end
            if (!Properties.Resources.UseWordWrap.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
            {
                System.Console.WriteLine(strText);
                return;
            }

            // first split the lines, if there are several lines
            if (strText.Contains("\n"))
            {
                // then write each line, wrapped
                foreach (string strLine in strText.Replace(Environment.NewLine, "\n").Split('\n'))
                {
                    WriteWrappedText(strLine.TrimEnd());
                }
            }
            else
            {
                // get somme information about the width of the window
                int nWindowWidth = Console.WindowWidth - 1;
                // and if we need to align the text at right, e.g. hebrew or arabic
                bool bRightToLeft = Properties.Resources.RightToLeft.Equals("Yes",
                    StringComparison.InvariantCultureIgnoreCase);

                // now process all words separately
                string[] aWords = strText.Split(' ');
                string strCurrentLine = "";

                foreach (string strWord in aWords)
                {
                    if ((strCurrentLine + strWord).Length < nWindowWidth)
                    {
                        strCurrentLine += strWord + " ";
                    }
                    else
                    {


                        if (bRightToLeft && !strCurrentLine.StartsWith("ResxVsCsv") &&
                            !strCurrentLine.StartsWith("  --") && !strCurrentLine.StartsWith("  [--"))
                        {
                            strCurrentLine = ReverseArabicAndHebrewText(strCurrentLine);

                            strCurrentLine = new string(' ', nWindowWidth - strCurrentLine.Length)
                                + (strCurrentLine.TrimEnd());
                        }
                        else
                            if (bRightToLeft)
                                strCurrentLine = ReverseArabicAndHebrewParts(strCurrentLine);

                        Console.WriteLine(strCurrentLine);

                        strCurrentLine = strWord + " ";
                    }
                }

                // the remaining part of the string
                if (strCurrentLine.Length > 0)
                {

                    if (bRightToLeft && !strCurrentLine.StartsWith("ResxVsCsv") &&
                        !strCurrentLine.StartsWith("  --") && !strCurrentLine.StartsWith("  [--"))
                    {
                        strCurrentLine = ReverseArabicAndHebrewText(strCurrentLine);

                        strCurrentLine = new string(' ', nWindowWidth - strCurrentLine.Length)
                                       + (strCurrentLine.TrimEnd());
                    }
                    else
                    {
                        if (bRightToLeft)
                        {
                            strCurrentLine = ReverseArabicAndHebrewParts(strCurrentLine);
                        }
                    }


                    Console.WriteLine(strCurrentLine);
                }
            }
        }


        //===================================================================================================
        /// <summary>
        /// This reverses the complete text, except non-arabic and non-hebrew parts, useful for rtl languages
        /// </summary>
        /// <param name="strInput">Input text</param>
        /// <returns>Reversed text</returns>
        //===================================================================================================
        static string ReverseArabicAndHebrewText(
            string strInput
            )
        {
            // first reverse the complete line
            char[] aChars = strInput.ToCharArray();
            Array.Reverse(aChars);
            for (int i = aChars.Length - 1; i >= 0; --i)
                switch (aChars[i])
                {
                    case '(':
                        aChars[i] = ')';
                        break;
                    case ')':
                        aChars[i] = '(';
                        break;
                    case '/':
                        aChars[i] = '\\';
                        break;
                    case '\\':
                        aChars[i] = '/';
                        break;
                }
            string strAllReversed = new string(aChars);

            // now reverse non-arabic and non-hebrew back to the normal order
            string strPattern = "[^\u0590-\u05FF\u0600-\u06FF:\\s()\\\"]+";
            return Regex.Replace(strAllReversed, strPattern, new MatchEvaluator(ReverseMatch));
        }


        //===================================================================================================
        /// <summary>
        /// This reverses hebrew and arabic parts, useful for ltr languages
        /// </summary>
        /// <param name="strInput">Input text</param>
        /// <returns>Text with reversed hebrew and arabic parts</returns>
        //===================================================================================================
        static string ReverseArabicAndHebrewParts(
            string strInput
            )
        {
            string strPattern = "[\\u0590-\\u05FF\\u0600-\\u06FF\\s,:\\.\\\"\\'\\(\\)]+";
            return Regex.Replace(strInput, strPattern, new MatchEvaluator(ReverseMatch));
        }


        //===================================================================================================
        /// <summary>
        /// Reverses a regex match
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        //===================================================================================================
        static string ReverseMatch(
            Match oMatch
            )
        {
            char[] aChars = oMatch.Value.ToCharArray();
            Array.Reverse(aChars);
            for (int i = aChars.Length - 1; i >= 0; --i)
                switch (aChars[i])
                {
                    case '(':
                        aChars[i] = ')';
                        break;
                    case ')':
                        aChars[i] = '(';
                        break;
                    case '/':
                        aChars[i] = '\\';
                        break;
                    case '\\':
                        aChars[i] = '/';
                        break;
                }
            return new string(aChars);
        }

        //===================================================================================================
        /// <summary>
        /// Reads a resx file and converts its strings to entries
        /// </summary>
        /// <param name="strFilePath">Path to read</param>
        /// <param name="strCulture">Culure for the entries</param>
        /// <param name="bOnlyStrings">Tells to skip all element with type="..." specification</param>
        /// <returns>An enumeration of strings in the resx</returns>
        //===================================================================================================
        static IEnumerable<Entry> ReadResxFile(
            string strFilePath,
            string strCulture,
            bool bOnlyStrings
            )
        {
            XDocument oXmlDoc = XDocument.Load(strFilePath);

            if (oXmlDoc.Root != null)
            {
                IEnumerable<XElement> iDataElements = oXmlDoc.Root.Elements("data");
                foreach (var oElement in iDataElements)
                {
                    // skip non-strings, if specified
                    XAttribute? oTypeAttribute = oElement.Attribute("type");

                    if (bOnlyStrings && oTypeAttribute != null)
                        continue;

                    var strType = oTypeAttribute?.Value;
                    XAttribute? oMimeTypeAttribute = oElement.Attribute("mimetype");
                    var strMimeType = oMimeTypeAttribute?.Value;

                    XAttribute? oNameAttribute = oElement.Attribute("name");
                    var strName = oNameAttribute?.Value;


                    // skip non-strings, if specified
                    if (bOnlyStrings && strName != null && strName.StartsWith(">>"))
                        continue;

                    var oValueNode = oElement.Element("value");
                    var oValueTextNode = oValueNode != null ? oValueNode.FirstNode as XText : null;
                    var strValue = oValueTextNode?.Value;
                    var oCommentNode = oElement.Element("comment");
                    var oCommentTextNode = oCommentNode != null ? oCommentNode.FirstNode as XText : null;
                    var strComment = oCommentTextNode?.Value;

 
                    yield return new Entry
                    {
                        Name = strName,
                        Value = strValue,
                        Comment = strComment,
                        Culture = strCulture,
                        Type = strType != null? (strType.Equals("System.Resources.ResXNullRef, System.Windows.Forms")?null:strType) : null,
                        MimeType = strMimeType
                    };
                }
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
            string strFilePath
            )
        {
            using (System.IO.StreamWriter oWriter =
                new StreamWriter(strFilePath, false, Encoding.UTF8))
            {
                oWriter.WriteLine("Culture;Name;Value;Comment;Type;MimeType");

                foreach (Entry oEntry in iEntries)
                {
                    oWriter.WriteLine(
                        ToCsv(oEntry.Culture) + ";" +
                        ToCsv(oEntry.Name) + ";" +
                        ToCsv(oEntry.Value) + ";" +
                        ToCsv(oEntry.Comment) + ";" +
                        ToCsv(oEntry.Type) + ";" +
                        ToCsv(oEntry.MimeType));
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
        static string ToCsv(
            string? strValue
            )
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
        /// Transforms a value t Json format
        /// </summary>
        /// <param name="strValue">Value to transform</param>
        /// <returns>Value in Json format</returns>
        //===================================================================================================
        static string ToJson(
            string strValue
            )
        {
            var oResult = new StringBuilder();

            oResult.Append("\"");
            foreach (char c in strValue)
            {
                switch (c)
                {
                    case '\\':
                        oResult.Append("\\\\");
                        break;
                    case '\"':
                        oResult.Append("\\\"");
                        break;
                    case '\b':
                        oResult.Append("\\b");
                        break;
                    case '\f':
                        oResult.Append("\\f");
                        break;
                    case '\n':
                        oResult.Append("\\n");
                        break;
                    case '\r':
                        oResult.Append("\\r");
                        break;
                    case '\t':
                        oResult.Append("\\t");
                        break;
                    default:
                        if (char.IsControl(c))
                        {
                            oResult.Append(
                                string.Format("\\u{0:X4}", (int)c));
                        }
                        else
                        {
                            oResult.Append(c);
                        }
                        break;
                }
            }
            oResult.Append("\"");

            return oResult.ToString();
        }

        //===================================================================================================
        /// <summary>
        /// Extracts JSon value from string
        /// </summary>
        /// <param name="strJson">JSon result</param>
        /// <param name="strKey">Key to search for</param>
        /// <returns>Value, transformed back</returns>
        //===================================================================================================
        static string ExtractJsonValue(
            string strJson,
            string strKey
            )
        {
            string pattern = "\\\"" + strKey + "\\\":\\\"(.*?)\\\"";
            Match match = Regex.Match(strJson, pattern);
            if (match.Success)
            {
                string strResult = match.Groups[1].Value
                    .Replace("\\n", "\n")
                    .Replace("\\t", "\t")
                    .Replace("\\\"", "\"")
                    .Replace("\\\\", "\\");


                // Replace Unicode escape sequences
                strResult = Regex.Replace(strResult, @"\\u([0-9A-Fa-f]{4})", match2 =>
                {
                    return ((char)Convert.ToInt32(match2.Groups[1].Value, 16)).ToString();
                });

                return strResult;
            }
            return string.Empty;
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
                string? strLine;
                string? strPartialLine = null;
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

                    for (int i = astrValues.Length - 1; i >= 0; --i)
                        astrValues[i] = astrValues[i].Replace("\"\"", "\"");

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
                                case "Type":
                                    oColumnHeadersDictionary[eColumn.Type] = i;
                                    break;
                                case "MimeType":
                                    oColumnHeadersDictionary[eColumn.MimeType] = i;
                                    break;
                            }
                        }

                        // someone deleted the headers?
                        if (oColumnHeadersDictionary.Count < 4 && astrValues.Length >= 4)
                        {
                            oColumnHeadersDictionary[eColumn.Culture] = 0;
                            oColumnHeadersDictionary[eColumn.Name] = 1;
                            oColumnHeadersDictionary[eColumn.Value] = 2;
                            oColumnHeadersDictionary[eColumn.Comment] = 3;
                            oColumnHeadersDictionary[eColumn.MimeType] = 4;
                            bHeaders = false;
                        }
                    };

                    if (!bHeaders)
                    {
                        yield return new Entry
                        {
                            Culture = oColumnHeadersDictionary.ContainsKey(eColumn.Culture) &&
                                   oColumnHeadersDictionary[eColumn.Culture] < astrValues.Length ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Culture]] :
                                    null,
                            Name = oColumnHeadersDictionary.ContainsKey(eColumn.Name) &&
                                   oColumnHeadersDictionary[eColumn.Name] < astrValues.Length ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Name]] :
                                    null,
                            Value = oColumnHeadersDictionary.ContainsKey(eColumn.Value) &&
                                   oColumnHeadersDictionary[eColumn.Value] < astrValues.Length ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Value]] :
                                    null,
                            Comment = oColumnHeadersDictionary.ContainsKey(eColumn.Comment) &&
                                   oColumnHeadersDictionary[eColumn.Comment] < astrValues.Length ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Comment]] :
                                    null,
                            Type = oColumnHeadersDictionary.ContainsKey(eColumn.Type) &&
                                   oColumnHeadersDictionary[eColumn.Type] < astrValues.Length ?
                                    astrValues[oColumnHeadersDictionary[eColumn.Type]] :
                                    null,
                            MimeType = oColumnHeadersDictionary.ContainsKey(eColumn.MimeType) &&
                                   oColumnHeadersDictionary[eColumn.MimeType] < astrValues.Length ?
                                    astrValues[oColumnHeadersDictionary[eColumn.MimeType]] :
                                    null
                        };
                    }
                    else
                        bHeaders = false;
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
            foreach (string strValue in astrValues)
            {
                if (strValue.Replace("\"\"","").StartsWith("\"") && (!strValue.Replace("\"\"","").EndsWith("\"") || 
                    ((strValue.Length-strValue.Replace("\"","").Length)&1) != 0))
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

            return oMatches.Cast<Match>().Select(m => m.Groups["value"].Value).ToArray();
        }

        //===================================================================================================
        /// <summary>
        /// Writes entries to a resx file, completely overwriting it
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
                new XElement("root",iEntries
                    .Where(static row => row.Name != null)
                    .Cast<Entry>()
                    .Select(static row =>
                        new XElement("data",
                            new XAttribute("name", row.Name??""),
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
        /// <param name="strTemplateResxPath">Path to template resx file</param>
        /// <param name="oFonts">All fonts from the CSV</param>
        //===================================================================================================
        public static void UpdateResxFile(
            IEnumerable<Entry> iNewValues,
            string strFilePath,
            string strTemplateResxPath,
            Dictionary<string, string> oFonts
            )
        {
            try
            {

                XDocument? oXmlDoc = null;

                try
                {
                    oXmlDoc = XDocument.Load(strFilePath);
                }
                catch
                {
                    // if the file isn't there we will try to copy hull from a different one
                    try
                    {
                        RemoveAllDataElements(strTemplateResxPath, strFilePath);
                    }
                    catch
                    {
                        // if failed - ignore
                    }

                    // try to load again, this time the excepion will be passed to the caller, if thrown
                    oXmlDoc = XDocument.Load(strFilePath);
                }

                foreach (var oNewValue in iNewValues)
                {
                    if (string.IsNullOrEmpty(oNewValue.Name))
                        continue;

                    var oElement = oXmlDoc.Root?.Elements("data")
                        .FirstOrDefault(e => e.Attribute("name")?.Value?.Equals(oNewValue.Name)??false);

                    if (oElement != null)
                    {
                        XElement? oValueElement = oElement.Element("value");
                        if (oValueElement != null && oNewValue.Value != null)
                        {
                            oValueElement.Value = oNewValue.Value;

                            // remove type attribute, if it is exactly the specified value
                            var oTypeAttr = oElement.Attribute("type");
                            if (oTypeAttr != null && oTypeAttr.Value.Equals("System.Resources.ResXNullRef, System.Windows.Forms"))
                            {
                                oTypeAttr.Remove();
                            }

                            // insert xml:space="preserve", if it is missing
                            var oSpaceAttr = oElement.Attribute(XNamespace.Xml + "space");
                            if (oSpaceAttr == null)
                            {
                                oElement.Add(new XAttribute(XNamespace.Xml + "space", "preserve"));
                            }
                        }

                        XElement? oCommentElement = oElement.Element("comment");
                        if (oCommentElement != null && oNewValue.Comment != null)
                        {
                            oCommentElement.Value = oNewValue.Comment;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(oNewValue.Comment) &&
                                !oNewValue.Comment.StartsWith(c_strSpecialComment))
                            {
                                oElement.Add(new XElement("comment", oNewValue.Comment));
                            }
                        }
                    }
                    else
                    {
                        XElement oNewElement = new XElement("data",
                            new XAttribute(XNamespace.Xml + "space", "preserve"),
                            new XAttribute("name", oNewValue.Name),
                            new XElement("value", oNewValue.Value
                            ));

                        if (!string.IsNullOrEmpty(oNewValue.Comment) &&
                            !oNewValue.Comment.StartsWith(c_strSpecialComment))
                        {
                            oNewElement.Add(new XElement("comment", oNewValue.Comment));
                        }

                        if (!string.IsNullOrEmpty(oNewValue.Type))
                        {
                            oNewElement.Add(new XAttribute("type", oNewValue.Type));
                        }

                        if (!string.IsNullOrEmpty(oNewValue.MimeType))
                        {
                            oNewElement.Add(new XAttribute("mimetype", oNewValue.MimeType));
                        }
                        oXmlDoc.Root?.Add(oNewElement);
                    }

                    // if we have a size of an element, assume there must be a font, as well
                    if (oNewValue.Name.EndsWith(".Size") &&
                        !oFonts.ContainsKey(oNewValue.Culture+"_"+oNewValue.Name.Replace(".Size",".Font")))
                    {
                        string? strNewValue = null;
                        if (oFonts.ContainsKey("(default)_" + oNewValue.Name.Replace(".Size", ".Font")))
                        {
                            strNewValue = oFonts["(default)_" + oNewValue.Name.Replace(".Size", ".Font")];
                        } else
                        if (oFonts.ContainsKey(oNewValue.Culture + "_$this.Font"))
                        {
                            strNewValue = oFonts[oNewValue.Culture + "_$this.Font"];
                        } else
                        if (oFonts.ContainsKey("(default)_$this.Font"))
                        {
                            strNewValue = oFonts["(default)_$this.Font"];
                        }

                        if (strNewValue != null)
                        {
                            oXmlDoc.Root?.Add(new XElement("data",
                                new XAttribute("name", oNewValue.Name.Replace(".Size", ".Font")),
                                new XAttribute("type", "System.Drawing.Font, System.Drawing"),
                                new XElement("value", strNewValue),
                                new XElement("comment", "Automatically fixed")
                                ));
                        }
                    }


                }


                System.Xml.XmlWriterSettings oXmlSettings = new System.Xml.XmlWriterSettings
                {
                    Encoding = new UTF8Encoding(false), // false means no BOM
                    Indent = true
                };

                using (System.IO.FileStream oStream = new FileStream(strFilePath, FileMode.Create, FileAccess.Write))
                using (System.Xml.XmlWriter oWriter = System.Xml.XmlWriter.Create(oStream, oXmlSettings))
                {
                    oXmlDoc.Save(oWriter);
                }
                //oXmlDoc.Save(strFilePath);
            }
            catch (Exception oEx)
            {
                WriteWrappedText(oEx.Message);
            }

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
            string? strApiKey,
            string strService,
            string? strApiUrl
            )
        {
#if TEST_TRANSLATION_LOGIC
            if (m_oTranslations != null)
            {
                foreach (Translation oTranslation in m_oTranslations)
                {
                    if (oTranslation.Language.Equals(strTargetLanguage))
                    {
                        return oTranslation.Text;
                    }
                }

                return m_oTranslations[0].Text;
            }
#endif
            switch (strService.ToLower())
            {
                case "google":
                    return TranslateWithGoogle(strText, strSourceLanguage, strTargetLanguage, strApiKey??"");
                case "microsoft":
                    return TranslateWithMicrosoft(strText, strSourceLanguage, strTargetLanguage, strApiKey??"");
                case "deepl":
                    return TranslateWithDeepL(strText, strSourceLanguage, strTargetLanguage, strApiKey??"");
                case "toptranslation":
                    return TranslateWithTopTranslation(strText, strSourceLanguage, strTargetLanguage, strApiKey??"");
                case "argos":
                    return TranslateWithArgosTranslate(strText, strSourceLanguage, strTargetLanguage, "");
                case "libretranslate":
                    return TranslateWithLibreTranslate(strText, strSourceLanguage, strTargetLanguage, strApiKey??"", strApiUrl??"");
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
            string strAPIKey
            )
        {
            string oUrl = @"https://translation.googleapis.com/language/translate/v2?key=" +
                strAPIKey + "&q=" + Uri.EscapeDataString(strText) + "&source=" + strSourceLanguage +
                "&target=" + strTargetLanguage;

            using (WebClient oWebClient = new WebClient())
            {
                oWebClient.Encoding = Encoding.UTF8;
                string oResponse = oWebClient.DownloadString(oUrl);
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
            string strAPIKey
            )
        {
            string oUrl = @"https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from=" +
                strSourceLanguage + "&to=" + strTargetLanguage;

            using (WebClient oWebClient = new WebClient())
            {
                oWebClient.Encoding = Encoding.UTF8;
                oWebClient.Headers.Add("Ocp-Apim-Subscription-Key", strAPIKey);
                oWebClient.Headers.Add("Content-Type", "application/json");

                string strBody = "[{\"Text\":" + ToJson(strText) + "}]";
                string strResponse = oWebClient.UploadString(oUrl, strBody);
                return ExtractTranslatedTextFromMicrosoftResponse(strResponse);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Translates a string with deepl engine
        /// </summary>
        /// <param name="strText">Text to translate</param>
        /// <param name="strSourceLanguage">Source language</param>
        /// <param name="strTargetLanguage">Target language</param>
        /// <param name="strAPIKey">Key</param>
        /// <returns>Translated string</returns>
        //===================================================================================================
        private static string TranslateWithDeepL(
            string strText,
            string strSourceLanguage,
            string strTargetLanguage,
            string strAPIKey
            )
        {
            string oUrl = @"https://api.deepl.com/v2/translate?auth_key=" + strAPIKey + "&text=" +
                Uri.EscapeDataString(strText) + "&source_lang=" + strSourceLanguage + "&target_lang=" +
                strTargetLanguage;

            using (WebClient oWebClient = new WebClient())
            {
                oWebClient.Encoding = Encoding.UTF8;
                string response = oWebClient.DownloadString(oUrl);
                return ExtractTranslatedTextFromDeepLResponse(response);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Translates a string with top translation engine
        /// </summary>
        /// <param name="strText">Text to translate</param>
        /// <param name="strSourceLanguage">Source language</param>
        /// <param name="strTargetLanguage">Target language</param>
        /// <param name="strAPIKey">Key</param>
        /// <returns>Translated string</returns>
        //===================================================================================================
        private static string TranslateWithTopTranslation(
            string strText,
            string strSourceLanguage,
            string strTargetLanguage,
            string strAPIKey
            )
        {
            string url = @"https://api.toptranslation.com/translate?auth_key=" + strAPIKey + "&text=" +
                Uri.EscapeDataString(strText) + "&source_lang=" + strSourceLanguage + "&target_lang=" + strTargetLanguage;

            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                string response = webClient.DownloadString(url);
                return ExtractTranslatedTextFromTopTranslationResponse(response);
            }
        }



        //===================================================================================================
        /// <summary>
        /// Translates a string with argos engine
        /// </summary>
        /// <param name="strText">Text to translate</param>
        /// <param name="strSourceLanguage">Source language</param>
        /// <param name="strTargetLanguage">Target language</param>
        /// <returns>Translated string</returns>
        //===================================================================================================
        private static string TranslateWithArgosTranslate(
            string strText,
            string strSourceLanguage,
            string strTargetLanguage,
            string strArgosTranslatePath
            )
        {
            var oProcess = new Process
            {
                StartInfo = new ProcessStartInfo(
                    "argos-tranlate", "--from-lang " + strSourceLanguage + " --to-lang " + strTargetLanguage)
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true                  
                }
            };
            oProcess.Start();
            oProcess.StandardInput.Write(strText);
            oProcess.StandardInput.Flush();
            oProcess.StandardInput.Close();
            string strResult = oProcess.StandardOutput.ReadToEnd();
            oProcess.WaitForExit();
            return strResult.Trim();
        }


        //===================================================================================================
        /// <summary>
        /// Translates a string with libre translate engine
        /// </summary>
        /// <param name="strText">Text to translate</param>
        /// <param name="strSourceLanguage">Source language</param>
        /// <param name="strTargetLanguage">Target language</param>
        /// <returns>Translated string</returns>
        //===================================================================================================
        private static string TranslateWithLibreTranslate(
            string strText,
            string strSourceLanguage,
            string strTargetLanguage,
            string strApiKey,
            string strLibreTranslateUrl
            )
        {
            using (WebClient oClient = new WebClient())
            {
                var oValues = new System.Collections.Specialized.NameValueCollection
                {
                    { "q", strText },
                    { "source", strSourceLanguage },
                    { "target", strTargetLanguage }
                };

                if (!string.IsNullOrEmpty(strApiKey))
                    oValues.Add("api_key", strApiKey);


                byte[] aResponse = oClient.UploadValues(strLibreTranslateUrl + "/translate", oValues);
                string strResponseString = Encoding.UTF8.GetString(aResponse);
                return ExtractTranslatedTextFromLibreResponse(strResponseString);
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
            return ExtractJsonValue(strResponse, "translatedText");
        }

        //===================================================================================================
        /// <summary>
        /// Extracts result from response
        /// </summary>
        /// <param name="strResponse">Response from microsoft service</param>
        /// <returns>The translated text from response</returns>
        //===================================================================================================
        private static string ExtractTranslatedTextFromMicrosoftResponse(
            string strResponse
            )
        {
            // Basic string manipulation to extract the translated text
            return ExtractJsonValue(strResponse, "text");
        }


        //===================================================================================================
        /// <summary>
        /// Extracts result from response
        /// </summary>
        /// <param name="strResponse">Response from deepl service</param>
        /// <returns>The translated text from response</returns>
        //===================================================================================================
        private static string ExtractTranslatedTextFromDeepLResponse(
            string strResponse
            )
        {
            return ExtractJsonValue(strResponse, "text");
        }

        //===================================================================================================
        /// <summary>
        /// Extracts result from response
        /// </summary>
        /// <param name="strResponse">Response from toptanslation service</param>
        /// <returns>The translated text from response</returns>
        //===================================================================================================
        private static string ExtractTranslatedTextFromTopTranslationResponse(
            string strResponse
            )
        {
            // Basic string manipulation to extract the translated text
            return ExtractJsonValue(strResponse, "translatedText");
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
        /// Extracts translated text from libre result
        /// </summary>
        /// <param name="strJsonResponse">libre response</param>
        /// <returns>Translation</returns>
        //===================================================================================================
        private static string ExtractTranslatedTextFromLibreResponse(
            string strResponse
            )
        {
            return ExtractJsonValue(strResponse, "translatedText");
        }


        //===================================================================================================
        /// <summary>
        /// Calls a large language model (LLM) HTTP Endpoint and returns the contentt
        /// of the first choice in the response
        /// </summary>
        /// <param name="strLLMQuery">JSON payload that will be sent to LLM</param>
        /// <param name="strApiUrl">URL of the LLM API</param>
        /// <param name="strApiKey">API key that will be used for Bearer authentification</param>
        /// <param name="ostrResult">Output parameter that will receive the LLMM
        /// response text from choices[0].messges.content</param>
        /// <returns>true iff the call was successful</returns>
        //===================================================================================================
        public static bool CallLLM(
            string strLLMQuery,
            string strApiUrl,
            string strApiKey,
            out string ostrResult)
        {
            for (int nRepeat = 3; nRepeat > 0; --nRepeat)
            {
                using (HttpClient oHttpClient = new HttpClient())
                {
                    oHttpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Bearer", strApiKey);

                    StringContent oContent =
                        new StringContent(strLLMQuery, Encoding.UTF8, "application/json");

                    try
                    {
                        HttpResponseMessage oResponse =
                            oHttpClient.PostAsync(strApiUrl, oContent).Result;

                        if (!oResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine(string.Format(
                                Properties.Resources.LlmCallFailedMessage, oResponse.StatusCode));
                            continue;
                        }
                        else
                        {
                            string strApiResult = oResponse.Content.ReadAsStringAsync().Result;

                            try
                            {
                                using (JsonDocument oJsonDoc = JsonDocument.Parse(strApiResult))
                                {
                                    JsonElement oRoot = oJsonDoc.RootElement;

                                    // expect { "choices": [ { "message": { "content": "..." } } ] }
                                    JsonElement oChoices;

                                    if (!oRoot.TryGetProperty("choices", out oChoices) ||
                                        oChoices.ValueKind != JsonValueKind.Array ||
                                        oChoices.GetArrayLength() == 0
                                        )
                                    {
                                        Console.WriteLine(string.Format(
                                            Properties.Resources.LlmMisssingChoicesElementMessage,
                                            strApiResult));
                                        continue;
                                    }

                                    JsonElement oFirstChoice = oChoices[0];

                                    JsonElement oMessage;

                                    if (!oFirstChoice.TryGetProperty("message", out oMessage) ||
                                        oMessage.ValueKind != JsonValueKind.Object)
                                    {
                                        Console.WriteLine(string.Format(
                                            Properties.Resources.LlmInvalidMessageElementMessage,
                                            strApiResult));
                                        continue;
                                    }

                                    string? strMessage = oMessage.GetString();
                                    ostrResult = strMessage ?? string.Empty;

                                    return true;

                                }
                            } catch (JsonException oEx2)
                            {
                                Console.WriteLine(string.Format(
                                    Properties.Resources.LlmErrorParsingJsonMessage, oEx2.Message));
                                continue;
                            }
                        }

                    }
                    catch (Exception oEx)
                    {
                        if (oEx is AggregateException && oEx.InnerException != null)
                        {
                            Console.WriteLine(string.Format(
                                Properties.Resources.LlmHtttpResponseErrorMessage,
                                oEx.InnerException.Message));
                        }
                        else
                        {
                            Console.WriteLine(string.Format(
                                Properties.Resources.LlmHtttpResponseErrorMessage, 
                                oEx.Message));
                        }
                        continue;
                    }
                }
            }

            ostrResult = String.Empty;
            return false;

        }

        //===================================================================================================
        /// <summary>
        /// Gets hopefully the best translation, based on a set of texts in different languages
        /// </summary>
        /// <param name="iTranslations">The translations of the text in different languages</param>
        /// <param name="strTargetLanguage">The target language</param>
        /// <param name="strAPIKey">API Key for querying translations</param>
        /// <param name="bBruteForce">Indicates if all translation variants need to be searched</param>
        /// <returns>The hopefully one and only translation</returns>
        //===================================================================================================
        public static string? GetBestTranslation(
            IList<Translation> iTranslations,
            string strTargetLanguage,
            string? strApiKey,
            string strService,
            string? strApiUrl,
            bool bBruteForce,
            string strLLMModel,
            string strElementName,
            string strComment
            )
        {
            // separate handlin of LLM
            if (strService.Equals("llm", StringComparison.InvariantCultureIgnoreCase))
            {
                List<object> aMessages = new List<object>();

                aMessages.Add(new
                {
                    role = "system",
                    content = "You are an agent that automatically translates GUI elements. "+
                    "User will provide names and available translations of the element, perhaps " +
                    "also a comment to the element. You will provide best possible translattion, "+
                    "based on available information. Text passages in source texts like " +
                    "{1}, {2}, {3} etc mean insertings that will be done later, during runtime "+
                    "of application, those can be e.g. numbers or text passages. "+
                    "They need to be placed in a meaningful way in the result." +
                    "Please return the translation as text without any additional comments."
                });

                aMessages.Add(new
                {
                    role = "user",
                    content =
                        "The name of the element is "+strElementName+". "+
                        (!string.IsNullOrEmpty(strComment)?("My comment to element: "+strComment)+'.':"")+
                        "Please provide translation in the culture with IETF-code '" + strTargetLanguage + "'."
                });

                // add all awailable translations
                foreach (Translation oTranslation in iTranslations)
                {
                    aMessages.Add(new
                    {
                        role = "user",
                        content = "This is a translation in a culture with IETF-code '" + oTranslation.Language + "':" +
                        oTranslation.Text
                    });
                }

                var oRequest = new
                {
                    model = strLLMModel,
                    messages = aMessages.ToArray()
                };


                JsonSerializerOptions oOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };

                string strJson = JsonSerializer.Serialize(oRequest, oOptions);

                string strLLMResult;
                if (!CallLLM(strJson, strApiUrl??"", strApiKey??"", out strLLMResult))
                {
                    throw new ApplicationException();
                }
                return strLLMResult;
            }
            else
            {
                string? strBestTranslation = null;
                int nMinEditDistance = int.MaxValue;
                float fMinSumOfEditDistances = float.MaxValue;

                foreach (Translation oTranslation in iTranslations)
                {
                    if (oTranslation.Language == null || oTranslation.Text == null)
                        continue;

                    string strTranslatedText = Translate(oTranslation.Language, oTranslation.Text,
                        strTargetLanguage, strApiKey, strService, strApiUrl);

                    if (!bBruteForce)
                    {
                        // if not in bruteforce method then we search for a translation that 
                        // back-translates to itself
                        string strBackTranslatedText = Translate(strTargetLanguage, strTranslatedText,
                            oTranslation.Language, strApiKey, strService, strApiUrl);

                        if (strBackTranslatedText.Equals(oTranslation.Text))
                        {
                            // Found a perfect match
                            return strTranslatedText;
                        }
                        else
                        {
                            int nEditDistance = GetEditDistance(oTranslation.Text, strBackTranslatedText);
                            if (nEditDistance < nMinEditDistance)
                            {
                                nMinEditDistance = nEditDistance;
                                strBestTranslation = strTranslatedText;
                            }
                        }
                    }
                    else
                    {
                        // in bruteforce mode we search for a translation that back-translates
                        // to same in possibly all available languages
                        float fSumOfWeightedDistances = 0;
                        foreach (var oTranslation2 in iTranslations)
                        {
                            if (oTranslation2.Language == null)
                                continue;

                            string strBackTranslatedText = Translate(strTargetLanguage, strTranslatedText,
                                oTranslation2.Language, strApiKey, strService, strApiUrl);

                            if (!strBackTranslatedText.Equals(oTranslation2.Text))
                            {
                                int nEditDistance = GetEditDistance(oTranslation.Text, strBackTranslatedText);
                                if (iTranslations.Count >= 10 && (
                                    oTranslation2.Language.StartsWith("en") ||
                                    oTranslation2.Language.StartsWith("es") ||
                                    oTranslation2.Language.StartsWith("de") ||
                                    oTranslation2.Language.StartsWith("pt") ||
                                    oTranslation2.Language.StartsWith("it") ||
                                    oTranslation2.Language.StartsWith("nl") ||
                                    oTranslation2.Language.StartsWith("no") ||
                                    oTranslation2.Language.StartsWith("fi") ||
                                    oTranslation2.Language.StartsWith("fr") ||
                                    oTranslation2.Language.StartsWith("da") ||
                                    oTranslation2.Language.StartsWith("ru") ||
                                    oTranslation2.Language.StartsWith("pl") ||
                                    oTranslation2.Language.StartsWith("ko") ||
                                    oTranslation2.Language.StartsWith("ja") ||
                                    oTranslation2.Language.StartsWith("zh") ||
                                    oTranslation2.Language.StartsWith("sa") ||
                                    oTranslation2.Language.StartsWith("hi")))
                                {
                                    // major languages have bigger weight of edit distances, 
                                    // if there are at least 10 translations present
                                    fSumOfWeightedDistances += 20 * nEditDistance /
                                        (float)(oTranslation.Text.Length + strBackTranslatedText.Length + 1);
                                }
                                else
                                {
                                    // for all other languages weight the distance based on the length of the text,
                                    // so the difference in length of the texts between languages doesn't play a role.
                                    fSumOfWeightedDistances += 2 * nEditDistance /
                                        (float)(oTranslation.Text.Length + strBackTranslatedText.Length + 1);
                                }
                            }
                        }

                        if (fSumOfWeightedDistances == 0)
                        {
                            return strTranslatedText;
                        }

                        if (fSumOfWeightedDistances < fMinSumOfEditDistances)
                        {
                            fMinSumOfEditDistances = fSumOfWeightedDistances;
                            strBestTranslation = strTranslatedText;
                        }

                    }
                }


                return strBestTranslation;
            }
        }

        //===================================================================================================
        /// <summary>
        /// Calculate the edit distance between two strings
        /// </summary>
        /// <param name="str1">first string</param>
        /// <param name="str2">second string</param>
        /// <returns>The edit distance</returns>
        //===================================================================================================
        private static int GetEditDistance(
            string str1,
            string str2
            )
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
                    aDist[i, j] = Math.Min(Math.Min(aDist[i - 1, j] + 1,
                        aDist[i, j - 1] + 1), aDist[i - 1, j - 1] + nCost);
                }
            }

            return aDist[str1.Length, str2.Length];
        }

        //===================================================================================================
        /// <summary>
        /// Loads a resx document without all the data elements in it
        /// </summary>
        /// <param name="strBaseResxPath">Path to load</param>
        /// <param name="strDestResxPath">Path to save</param>
        //===================================================================================================
        private static void RemoveAllDataElements(
            string strBaseResxPath,
            string strDestResxPath
            )
        {
            // Load the base RESX data into a list
            XmlDocument oBaseResxDoc = new XmlDocument();
            oBaseResxDoc.PreserveWhitespace = true;
            oBaseResxDoc.Load(strBaseResxPath);

            // all data elements are to remove
            List<XmlNode> aElementsToRemove = LoadDataElements(oBaseResxDoc);
            RemoveElementsFromXmlDoc(oBaseResxDoc, aElementsToRemove);

            oBaseResxDoc.Save(strDestResxPath);
        }

        //===================================================================================================
        /// <summary>
        /// Removes duplicate elements from other resx files
        /// </summary>
        /// <param name="strBaseResxPath">Path of resx of base(default) culture</param>
        /// <param name="iOtherResxFiles">Pathes of other resx files</param>
        //===================================================================================================
        private static void RemoveDuplicateDataElements(
            string strBaseResxPath, 
            IEnumerable<string> iOtherResxFiles
            )
        {
            // Load the base RESX data into a list
            XmlDocument oBaseResxDoc = new XmlDocument();
            oBaseResxDoc.PreserveWhitespace = true;
            oBaseResxDoc.Load(strBaseResxPath);

            List<XmlNode> aBaseDocElements = LoadDataElements(oBaseResxDoc);

            // Process each of the other RESX files
            foreach (string strOtherResxPath in iOtherResxFiles)
            {
                if (strOtherResxPath.Equals(strBaseResxPath))
                {
                    continue;
                }

                XmlDocument oOtherResxDoc = new XmlDocument();
                oOtherResxDoc.PreserveWhitespace = true;
                oOtherResxDoc.Load(strOtherResxPath);

                List<XmlNode> aOtherDocElements = LoadDataElements(oOtherResxDoc);
                List<XmlNode> aOtherDocElementsToRemove = new List<XmlNode>();

                foreach (XmlNode oOtherResxDataElement in aOtherDocElements)
                {
                    foreach (XmlNode oBaseResxDataElement in aBaseDocElements)
                    {
                        if (AreDataElementsEqual(oBaseResxDataElement, oOtherResxDataElement))
                        {
                            aOtherDocElementsToRemove.Add(oOtherResxDataElement);
                            break;
                        }
                    }
                }

                // Remove matching elements
                RemoveElementsFromXmlDoc(oOtherResxDoc, aOtherDocElementsToRemove);

                System.Xml.XmlWriterSettings oXmlSettings = new System.Xml.XmlWriterSettings
                {
                    Encoding = new UTF8Encoding(false), // false means no BOM
                    Indent = true
                };

                using (System.IO.FileStream oStream = new FileStream(strOtherResxPath, FileMode.Create, FileAccess.Write))
                using (System.Xml.XmlWriter oWriter = System.Xml.XmlWriter.Create(oStream, oXmlSettings))
                {
                    oOtherResxDoc.Save(oWriter);
                }

            }
        }

        //===================================================================================================
        /// <summary>
        /// Gets all data elements in document
        /// </summary>
        /// <param name="oResxDoc">Document to search for data nodes</param>
        /// <returns>A list of data-elementnodes</returns>
        //===================================================================================================
        static List<XmlNode> LoadDataElements(
            XmlDocument oResxDoc
            )
        {
            List<XmlNode> dataElements = new List<XmlNode>();

            foreach (XmlNode oDataElementNode in oResxDoc.GetElementsByTagName("data"))
            {
                dataElements.Add(oDataElementNode);
            }

            return dataElements;
        }

        //===================================================================================================
        /// <summary>
        /// Returns inner text of a node
        /// </summary>
        /// <param name="oNode">Node, can also be null</param>
        /// <returns>the inner text, or null in case oNode is null</returns>
        //===================================================================================================
        static string? GetInnerText(
            XmlNode? oNode
            )
        {
            if (oNode == null)
            {
                return null;
            }

            return oNode.InnerText;
        }

        //===================================================================================================
        /// <summary>
        /// Compares two strings for equality
        /// </summary>
        /// <param name="str1">First string (can also be null)</param>
        /// <param name="str2">Second string (can also be null)</param>
        /// <returns>true iff both strings are equal</returns>
        //===================================================================================================
        static bool StringsEqual(
            string? str1, 
            string? str2
            )
        {
            if (str1 == null || str2 == null)
            {
                return str1 == null && str2 == null;
            }

            return str1.Equals(str2);
        }

        //===================================================================================================
        /// <summary>
        /// Compares inner text of nodes for equality
        /// </summary>
        /// <param name="oNode1">Node from one XML document</param>
        /// <param name="oNode2">Node from another XML document</param>
        /// <returns>true iff the inner text of nodes equals, or both nodes are null</returns>
        //===================================================================================================
        static bool InnerTextEqual(
            XmlNode? oNode1, 
            XmlNode? oNode2
            )
        {
            return StringsEqual(GetInnerText(oNode1), GetInnerText(oNode2));
        }

        //===================================================================================================
        /// <summary>
        /// Compares two data elements for equality
        /// </summary>
        /// <param name="oBaseDocElement">Element from base doc</param>
        /// <param name="oOotherDocElement">Element from other doc</param>
        /// <returns>true iff the elements can be considered equal</returns>
        //===================================================================================================
        static bool AreDataElementsEqual(
            XmlNode oBaseDocElement, 
            XmlNode oOotherDocElement
            )
        {
            // Compare name attribute
            if (!InnerTextEqual(oBaseDocElement.Attributes?["name"], oOotherDocElement.Attributes?["name"])) 
                return false;

            // Compare value sub-element
            if (!InnerTextEqual(oBaseDocElement.SelectSingleNode("value"), oOotherDocElement.SelectSingleNode("value")))
                return false;

            // Compare type attribute
            if (!InnerTextEqual(oBaseDocElement.Attributes?["type"], oOotherDocElement.Attributes?["type"]))
                return false;

            // Compare mimetype attribute
            return InnerTextEqual(oBaseDocElement.Attributes?["mimetype"], oOotherDocElement.Attributes?["mimetype"]);
        }

        //===================================================================================================
        /// <summary>
        /// Removes specified elements from XML document
        /// </summary>
        /// <param name="oResxDoc">Document for processing</param>
        /// <param name="aElementsToRemove">Nodes to remove</param>
        //===================================================================================================
        static void RemoveElementsFromXmlDoc(
            XmlDocument oResxDoc, 
            List<XmlNode> aElementsToRemove
            )
        {
            ArgumentNullException.ThrowIfNull(oResxDoc);
            foreach (XmlNode oNode in aElementsToRemove)
            {
                XmlNode? oParentNode = oNode.ParentNode;
                oParentNode?.RemoveChild(oNode);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Adds cultures to the list of distinct cultures, if they are missin there
        /// </summary>
        /// <param name="aDistinctCultures">A list of distinct cultures</param>
        /// <param name="iAddCultures">An enumeration of cultures to add</param>
        //===================================================================================================
        static void AddCulturesIfMissing(
            List<string> aDistinctCultures,
            IEnumerable<string> iAddCultures
            )
        {
            foreach (string strCulture in iAddCultures)
            {
                bool bFound = false;
                foreach (string strCulture2 in aDistinctCultures)
                {
                    if (strCulture.Equals(strCulture2))
                    {
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    aDistinctCultures.Add(strCulture);
                }
            }
        }
    }
}
