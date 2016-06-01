using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace LearnWordsFast.DAL.EF.Repositories
{
    public class UserRepository : IUserPasswordStore<User>, IUserEmailStore<User>
    {
        public void Dispose()
        {
        }

        private readonly Context _db;

        public UserRepository(Context db)
        {
            _db = db;
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email = userName);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToUpper());
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _db.Users.Add(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _db.Users.Update(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _db.Users.Remove(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(Users().FirstOrDefault(x => x.Id.ToString() == userId));
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult(Users().FirstOrDefault(x => x.Email.ToUpper() == normalizedUserName));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password != null);
        }

        private IIncludableQueryable<User, Language> Users()
        {
            return _db.Users
                .Include(x => x.Words)
                .Include(x => x.AdditionalLanguages)
                .Include(x => x.MainLanguage)
                .Include(x => x.TrainingLanguage);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email = email);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToUpper());
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(Users().FirstOrDefault(x => x.Email.ToUpper() == normalizedEmail));
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}