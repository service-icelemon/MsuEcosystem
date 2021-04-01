using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties.Identity
{
    public class UserServiceResponse
    {
        public bool Succeeded { get; set; }
        public MsuUser User { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
