using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class TblUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string EmailAddress { get; set; }
        public int Designation { get; set; }
        public int? SectionId { get; set; }
        public byte[] Photo { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool NeverExperied { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? GroupId { get; set; }
        public bool Status { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
        public DateTime? LastLockoutDate { get; set; }
        public int? FailedPasswordAttemptCount { get; set; }
        public string Comment { get; set; }
        public bool? ChangePasswordAtFirstLogin { get; set; }

        public virtual TblGroup Group { get; set; }
    }
}
