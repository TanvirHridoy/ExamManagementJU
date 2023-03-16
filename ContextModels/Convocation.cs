using System;
using System.Collections.Generic;

#nullable disable

namespace EMSJu.ContextModels
{
    public partial class Convocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public DateTime? ProgramDate { get; set; }
        public bool? Status { get; set; }
    }
}
