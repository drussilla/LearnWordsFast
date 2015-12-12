using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNet.Identity;

namespace LearnWordsFast.DAL.EF.Repositories
{
    public class UserRepository : IUserPasswordStore<User>
    {
        public void Dispose()
        {
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
            user.Email = userName;
            return Task.FromResult(0);
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
            using (var db = new Context())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Task.FromResult(IdentityResult.Success);
            }
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            using (var db = new Context())
            {
                db.Users.Update(user);
                db.SaveChanges();
                return Task.FromResult(IdentityResult.Success);
            }
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            using (var db = new Context())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Task.FromResult(IdentityResult.Success);
            }
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var db = new Context())
            {
                return Task.FromResult(db.Users.FirstOrDefault(x => x.Id.ToString() == userId));
            }
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var db = new Context())
            {
                return Task.FromResult(db.Users.FirstOrDefault(x => x.Email.ToUpper() == normalizedUserName));
            }
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
    }
}