/* MIT License

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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResxVsCsv
{
    //*******************************************************************************************************
    /// <summary>
    /// Represents an entry in the CSV or Resx
    /// </summary>
    //*******************************************************************************************************
    public class Entry
    {
        //===================================================================================================
        /// <summary>
        /// Culture of the entry
        /// </summary>
        public string? Culture { get; set; }

        //===================================================================================================
        /// <summary>
        /// The name of the entry
        /// </summary>
        public string? Name { get; set; }

        //===================================================================================================
        /// <summary>
        /// The value of the entry
        /// </summary>
        public string? Value { get; set; }

        //===================================================================================================
        /// <summary>
        /// The comment of the entry
        /// </summary>
        public string? Comment { get; set; }

        //===================================================================================================
        /// <summary>
        /// The type of the entry (if it is not a simple string
        /// </summary>
        public string? Type { get; set; }

        //===================================================================================================
        /// <summary>
        /// The type of the entry (if it is not a simple string
        /// </summary>
        public string? MimeType { get; set; }

        //===================================================================================================
        /// <summary>
        /// Compares name and culture
        /// </summary>
        /// <param name="obj">Other object</param>
        /// <returns>true iff the name and the culture are equal</returns>
        //===================================================================================================
        public override bool Equals(object? obj)
        {
            if (obj == this)
                return true;
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (Entry)obj;

            if ((Culture != null) != (other.Culture != null))
                return false;

            if ((Name != null) != (other.Name != null))
                return false;

            if (Culture != null)
            {
                if (!Culture.Equals(other.Culture))
                {
                    return false;
                }
            }


            if (Name != null)
            {
                if (!Name.Equals(other.Name))
                {
                    return false;
                }
            }

            return true;
        }

        //===================================================================================================
        /// <summary>
        /// Calculates a hash code, based on name and culture
        /// </summary>
        /// <returns>hash value</returns>
        //===================================================================================================
        public override int GetHashCode()
        {
            return (Culture != null ? Culture.GetHashCode() : 1) ^ (Name != null ? Name.GetHashCode() : 0);
        }
    }

}
