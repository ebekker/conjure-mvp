using System;
using System.Collections.Generic;
using System.Text;

namespace Conjure.Data
{
    public class FetchResult<TItem>
    {
        public int TotalCount { get; set; }

        public IEnumerable<TItem> Items { get; set; }
    }
}
