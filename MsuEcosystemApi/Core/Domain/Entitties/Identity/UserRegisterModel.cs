using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties.Identity
{
    public class UserRegisterModel
    {
        
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int FacultyId { get; set; }
        public string AvatarImage { get; set; }
        public int StudentCardId { get; set; }
        public int GroupNumber { get; set; }
        public bool IsTeacher { get; set; }
    }
}
