using System;
using System.Collections.Generic;
using System.Text;

namespace Conjure.Controls.AgGrid
{
    public class ColumnDefinition
    {
        public string Field { get; set; }
        public string HeaderName { get; set; }
        public bool Resizable { get; set; }
        public bool Sortable { get; set; }
        public bool Filter { get; set; }
    }
}
