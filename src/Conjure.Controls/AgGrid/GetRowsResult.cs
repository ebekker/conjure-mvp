using System;
using System.Collections.Generic;
using System.Text;

namespace Conjure.Controls.AgGrid
{
    public class GetRowsResult
    {
        public bool Success { get; set; }
        public int? LastRow { get; set; }
        public object[] BlockRows { get; set; }
    }
}
