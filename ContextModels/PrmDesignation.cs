using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class PrmDesignation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameB { get; set; }
        public string JobDescription { get; set; }
        public string Remarks { get; set; }
        public int? SortingOrder { get; set; }
        public string ShortName { get; set; }
    }
}
