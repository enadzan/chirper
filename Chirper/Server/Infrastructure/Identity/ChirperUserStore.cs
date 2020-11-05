using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Chirper.Server.DomainModel;
using Chirper.Server.Repositories;

namespace Chirper.Server.Infrastructure.Identity
{
    public class ChirperUserStore: 
        IUserStore<ChirpUser>, 
        IUserPasswordStore<ChirpUser>, 
        IUserSecurityStampStore<ChirpUser>
    {
        private readonly IChirpDb _db;

        public ChirperUserStore(IChirpDb db)
        {
            _db = db;
        }

        public void Dispose()
        {
        }

        public Task<string> GetUserIdAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Username);
        }

        public Task SetUserNameAsync(ChirpUser user, string userName, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.Username = userName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UsernameNormalized);
        }

        public Task SetNormalizedUserNameAsync(ChirpUser user, string normalizedName, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.UsernameNormalized = normalizedName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _db.Users.Add(user);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                if (e.IsPrimaryOrUniqueKeyViolation())
                {
                    return Task.FromResult(IdentityResult.Failed(new IdentityError
                        {Code = "err_username_taken", Description = $"Username {user.UsernameNormalized} is taken"})
                    );
                }

                return Task.FromResult(IdentityResult.Failed(new IdentityError
                    {Code = "err_internal_error", Description = e.Message})
                );
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var currentUser = _db.Users.Find(user.Id);
            if (currentUser == null)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                    {Code = "err_user_not_found", Description = $"User {user.Id} does not exist"})
                );
            }

            try
            {
                currentUser.Username = user.Username;
                currentUser.UsernameNormalized = user.UsernameNormalized;

                _db.SaveChanges();
            }
            catch (Exception e)
            {
                if (e.IsPrimaryOrUniqueKeyViolation())
                {
                    return Task.FromResult(IdentityResult.Failed(new IdentityError
                        {Code = "err_username_taken", Description = $"Username {user.UsernameNormalized} is taken"})
                    );
                }

                return Task.FromResult(IdentityResult.Failed(new IdentityError
                    {Code = "err_internal_error", Description = e.Message})
                );
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (!_db.Users.MarkDeleted(user.Id))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                    {Code = "err_user_not_found", Description = $"User {user.Id} does not exist"})
                );
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<ChirpUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = int.TryParse(userId, out var id) ? _db.Users.Find(id) : null;
            return Task.FromResult(user);
        }

        public Task<ChirpUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = _db.Users.FindByUsernameNormalized(normalizedUserName);
            return Task.FromResult(user);
        }

        public Task SetPasswordHashAsync(ChirpUser user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Password != "not_valid");
        }

        public Task SetSecurityStampAsync(ChirpUser user, string stamp, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        public Task<string> GetSecurityStampAsync(ChirpUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }
    }
}
