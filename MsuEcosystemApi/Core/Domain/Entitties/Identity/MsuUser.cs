using Microsoft.AspNetCore.Identity;

namespace Domain.Entitties.Identity
{
    public class MsuUser : IdentityUser
    {
        public string AvatarImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int StudentCardId { get; set; }
        public int GroupNumber { get; set; }
        public int FacultyId { get; set; }
        public bool IsTeacher { get; set; }
    }
}

