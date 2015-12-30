using System;
using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNet.Identity;

namespace LearnWordsFast.API.Services
{
    public class AspNetMvcUserManager : IUserManager
    {
        private readonly UserManager<User> _userManager;

        public AspNetMvcUserManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public Task<User> FindById(Guid id)
        {
            return _userManager.FindByIdAsync(id.ToString());
        }

        public Task<IdentityResult> UpdateAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }
    }
}