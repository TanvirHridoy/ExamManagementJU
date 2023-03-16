using EMSJu.ContextModels;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSJu.Models
{
    public class StudentVm
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
[NotMapped]
        public IFormFile Photo { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public int ProgramId { get; set; }
        public int BatchId { get; set; }

    }

    public class StudentCreateVm
    {
        public List<EMSJu.ContextModels.Program> Programs { get; set; }
        public List<EMSJu.ContextModels.BatchInfo> BatchInfos { get; set; }
        public StudentVm Student { get; set; }

    }
    public class StudentInfoVm
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public byte[] Photo { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public string  ProgramName { get; set; }
        public string BatchName { get; set; }
    }
}
