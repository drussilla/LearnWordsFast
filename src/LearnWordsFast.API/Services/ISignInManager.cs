using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNet.Identity;

namespace LearnWordsFast.API.Services
{
    public interface ISignInManager
    {
        Task<SignInResult> PasswordSignInAsync(string login, string password);
        Task SignOutAsync();
        Task SignInAsync(User user);
    }
}