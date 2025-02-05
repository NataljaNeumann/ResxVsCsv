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
        public string Culture { get; set; }

        //===================================================================================================
        /// <summary>
        /// The name of the entry
        /// </summary>
        public string Name { get; set; }

        //===================================================================================================
        /// <summary>
        /// The value of the entry
        /// </summary>
        public string Value { get; set; }

        //===================================================================================================
        /// <summary>
        /// The comment of the entry
        /// </summary>
        public string Comment { get; set; }

        //===================================================================================================
        /// <summary>
        /// Compares name and culture
        /// </summary>
        /// <param name="obj">Other object</param>
        /// <returns>true iff the name and the culture are equal</returns>
        //===================================================================================================
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (Entry)obj;
            return Culture.Equals(other.Culture) && Name.Equals(other.Name);
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
