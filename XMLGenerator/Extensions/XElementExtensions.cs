﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLGenerator.Extensions
{
    public static class XElementExtensions
    {
        /// <summary>
        /// Trims whitespace from the xml node values.  
        /// DOES NOT trim whitespace outside of values, can use PreserveWhitespace LoadOption when parsing for that.
        /// </summary>
        /// <param name="element"></param>
        public static void TrimWhiteSpaceFromValues(this XElement element)
        {
            foreach (var descendent in element.Descendants())
            {
                if (!descendent.HasElements)
                {
                    descendent.SetValue(descendent.Value.Trim());
                }
                else
                {
                    descendent.TrimWhiteSpaceFromValues();
                }
            }
        }
    }
}
