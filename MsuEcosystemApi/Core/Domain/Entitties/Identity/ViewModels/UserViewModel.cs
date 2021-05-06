using System.Collections.Generic;

namespace Domain.Entitties.Identity.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string AvatarImage { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int StudentCardId { get; set; }
        public int GroupNumber { get; set; }
        public int FacultyId { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsTeacher { get; set; }
    }
}
