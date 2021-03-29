﻿using Domain.Entitties.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationModel> GetTokenAsync(TokenRequest request); 
        Task<string> RegisterAsync(RegisterModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
