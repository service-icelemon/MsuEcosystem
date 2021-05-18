using Domain.Entitties.MsuInfo.ViewModels;
using System.Collections.Generic;

namespace Domain.Entitties.Identity.ViewModels
{
    public class UserViewModel
    {
        public string AccountId { get; set; }
        public StudentViewModel StudentData { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsTeacher { get; set; }
        public TeacherViewModel TeacherData { get; set; }
    }
}
