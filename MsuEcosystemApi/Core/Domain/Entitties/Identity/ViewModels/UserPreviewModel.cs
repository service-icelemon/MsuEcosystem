using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties.Identity.ViewModels
{
    public class UserPreviewModel
    {
        public string Id { get; set; }
        public string AvatarImageUrl { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
    }
}
