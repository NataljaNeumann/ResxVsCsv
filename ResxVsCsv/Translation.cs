using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResxVsCsv
{
    //*******************************************************************************************************
    /// <summary>
    /// Represents a translation
    /// </summary>
    //*******************************************************************************************************
    public class Translation
    {
        //===================================================================================================
        /// <summary>
        /// Language of a translation
        /// </summary>
        public string Language { get; set; }

        //===================================================================================================
        /// <summary>
        /// Text of the translation
        /// </summary>
        public string Text { get; set; }
    }
}
