using System;
using System.Collections.Generic;

namespace Conjure.Data
{
    public class QueryResultPage<TItem>
    {
        public int TotalCount { get; set; }

        public IEnumerable<TItem> PageItems { get; set; }
    }
}
