using Microsoft.AspNetCore.Identity;

namespace Domain.Entitties.Identity
{
    public class MsuUser : IdentityUser
    {
        public int StudentCardId { get; set; }
        public int TeacherCode { get; set; }
        public bool IsTeacher { get; set; }
    }
}

