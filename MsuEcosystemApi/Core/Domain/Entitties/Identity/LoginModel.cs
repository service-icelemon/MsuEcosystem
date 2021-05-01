using System.ComponentModel.DataAnnotations;

namespace Domain.Entitties.Identity
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
