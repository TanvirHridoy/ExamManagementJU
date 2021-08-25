using System;
using System.Collections.Generic;

#nullable disable

namespace CertificationMS.ContextModels
{
    public partial class CertApplication
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public bool ChangeNubCampus { get; set; }
        public int? FromNubCampus { get; set; }
        public int? ToNubCampus { get; set; }
        public int StudentType { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public int MajorSubject { get; set; }
        public bool RegisteredConv { get; set; }
        public int? ConvocationId { get; set; }
        public DateTime ApplyDate { get; set; }
        public string TrackId { get; set; }
        public int? ApprovedByDept { get; set; }
        public int? ApprovedByAcc { get; set; }
        public int? ApprovedByLib { get; set; }
        public int? ApprovedByAcad { get; set; }
        public int? ApprovedByExam { get; set; }
        public int? ApvStatusDept { get; set; }
        public int? ApvStatusAcc { get; set; }
        public int? ApvStatusLib { get; set; }
        public int? ApvStatusAcad { get; set; }
        public int? ApvStatusExam { get; set; }
        public DateTime? ApvDeptDate { get; set; }
        public DateTime? ApvAccDate { get; set; }
        public DateTime? ApvLibDate { get; set; }
        public DateTime? ApvAcaddate { get; set; }
        public DateTime? ApvExamDate { get; set; }
        public bool? IsDelivered { get; set; }
        public string ExtraOne { get; set; }
        public string ExtraTwo { get; set; }
        public string ExtraThree { get; set; }
        public string ExtraFour { get; set; }

        public virtual ApvStatus ApprovedByExamNavigation { get; set; }
        public virtual ApvStatus ApvStatusAcadNavigation { get; set; }
        public virtual ApvStatus ApvStatusAccNavigation { get; set; }
        public virtual ApvStatus ApvStatusDeptNavigation { get; set; }
        public virtual ApvStatus ApvStatusLibNavigation { get; set; }
        public virtual Convocation Convocation { get; set; }
        public virtual Campus FromNubCampusNavigation { get; set; }
        public virtual MajorSubject MajorSubjectNavigation { get; set; }
        public virtual StudentType StudentTypeNavigation { get; set; }
        public virtual Campus ToNubCampusNavigation { get; set; }
    }
}
