using System;
using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNet.Identity;

namespace LearnWordsFast.Services
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

        public Task<User> FIndById(Guid id)
        {
            return _userManager.FindByIdAsync(id.ToString());
        }
    }
}