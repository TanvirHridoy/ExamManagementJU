using EMSJu.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSJu.Models
{
    public class UsersViewModel
    {
        public List<UsersModel> List { get; set; }
        public List<PrmDesignation> ListDeg { get; set; }
        public string message { get; set; } = "";
    }
    public class UsersModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string EmailAddress { get; set; }
        public int Designation { get; set; }
        public int? GroupId { get; set; }
        public bool Status { get; set; }
    }
    public class UserCreateModel
    {
        public List<PrmDesignation> ListDesignations { get; set; }
        public List<Section> ListSection { get; set; }
        public List<TblGroup> ListGroup { get; set; }
        public UserCreateForm User { get; set; }
        public string message { get; set; } = "";

    }
    public class UserCreateForm
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
        public int? GroupId { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
    }
}
