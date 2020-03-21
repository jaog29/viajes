using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;    
using System.Threading.Tasks;
using Viajes.Web.Data.Entities;
using Viajes.Web.Models;

namespace Viajes.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();

    }

}
