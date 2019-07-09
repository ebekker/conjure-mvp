using System;
using System.Collections.Generic;
using System.Text;

namespace Conjure.Data
{
    public class FetchOptions
    {
        /// <summary>
        /// A string description of one or more columns to sort by.
        /// Multiple columns are separated by commas, with each
        /// column preceded by an optional descending sort indicator
        /// of the minus (-).
        /// </summary>
        public string Sort { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
