using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties.Identity
{
    public class AddRoleModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
