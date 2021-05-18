using System.ComponentModel.DataAnnotations;

namespace Domain.Entitties.Identity
{
    public class UserRegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int StudentCardId { get; set; }
        [Required]
        public bool IsTeacher { get; set; }
        public int TeacherCode { get; set; }
    }
}
