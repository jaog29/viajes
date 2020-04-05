using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Viajes.Common.Enums;
using Viajes.Web.Data.Entities;
using Viajes.Web.Models;

namespace Viajes.Web.Helpers
{
    public interface IUserHelper
    {
        Task<string> GeneratePasswordResetTokenAsync(UserEntity user);
        Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);
        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);
        Task<UserEntity> GetUserAsync(string email);
        Task<UserEntity> GetUserAsync(Guid userId);
        Task<IdentityResult> AddUserAsync(UserEntity user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(UserEntity user, string roleName);
        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);     
        Task LogoutAsync();
        Task<UserEntity> AddUserAsync(AddUserViewModel model);
    }

}
