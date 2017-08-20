namespace RoadShowHardCode.Api.Identity
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using RoadShowHardCode.BusinessLayer.Errors;
    using RoadShowHardCode.Models;

    /// <summary>
    /// The asp net identity service.
    /// </summary>
    public class AspNetIdentityService : IIdentityService
    {
        /// <summary>
        /// The storage.
        /// </summary>
        private readonly IIdentityStorage storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetIdentityService"/> class.
        /// </summary>
        public AspNetIdentityService()
            : this(new AspNetIdentityStorage())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetIdentityService"/> class.
        /// </summary>
        /// <param name="storage">
        /// The storage.
        /// </param>
        public AspNetIdentityService(IIdentityStorage storage)
        {
            this.storage = storage;
        }

        /// <summary>
        /// The change password.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="System.Threading.Tasks.Task"/>.
        /// </returns>
        public async Task<ServiceResult<bool>> ChangePasswordAsync(User user, string newPassword)
        {
            var identityUser = this.storage.GetUser(user.UserName);

            if (identityUser == null)
            {
                return new ServiceResult<bool>(new UserNotFoundError());
            }

            var helper = new SecurityHelper(this.storage.UserManager);
            var result = await helper.ValidatePasswordAsync(newPassword);
            if (!result.Succeeded)
            {
                return new ServiceResult<bool>(new InvalidPasswordError(result.Errors));
            }

            if (identityUser.PasswordResetToken != user.PasswordResetToken)
            {
                return new ServiceResult<bool>(new InvalidPasswordResetTokenError());
            }

            identityUser.PasswordResetToken = null;
            identityUser.PasswordHash = helper.HashPassword(newPassword);
            var updateResult = this.storage.Update(identityUser);

            return updateResult.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new ChangePasswordError(updateResult.Errors));
        }

        /// <summary>
        /// The change password.
        /// </summary>
        /// <param name="userLogin">
        /// The user login.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> ChangePassword(string userLogin, string newPassword)
        {
            var identityUser = this.storage.GetUser(userLogin);

            if (identityUser == null)
            {
                return new ServiceResult<bool>(new UserNotFoundError());
            }

            var helper = new SecurityHelper(this.storage.UserManager);
            var result = helper.ValidatePassword(newPassword);
            if (!result.Succeeded)
            {
                return new ServiceResult<bool>(new InvalidPasswordError(result.Errors));
            }

            identityUser.PasswordResetToken = null;
            identityUser.PasswordHash = helper.HashPassword(newPassword);
            var updateResult = this.storage.Update(identityUser);

            return updateResult.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new ChangePasswordError(updateResult.Errors));
        }

        /// <summary>
        /// The authorize.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<LoginResponse> OAuthAuthorize(LoginInfo login)
        {
            var user = this.storage.GetUser(login.Email, login.Password);

            if (user == null)
            {
                return new ServiceResult<LoginResponse>(new UserNotFoundError());
            }

            if (!user.EmailConfirmed)
            {
                return new ServiceResult<LoginResponse>(new AccountIsNotActivatedError());
            }

            var identity = this.storage.CreateIdentity(user, login.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Email, login.Email));

            return
                new ServiceResult<LoginResponse>(
                    new LoginResponse
                    {
                        Token = SecurityHelper.GenerateAccessToken(identity, login.SecureDataFormat),
                        Id = user.Id
                    });
        }

        /// <summary>
        /// The re issue credentials.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<LoginResponse> ReIssueCredentials(LoginInfo login)
        {
            var user = this.storage.GetUserById(login.Id);

            // if (user.IsReLoginRequired)
            // {
            // return new ServiceResult<LoginResponse>(new AccountIsNotActivatedError());
            // }
            var identity = this.storage.CreateIdentity(user, login.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Email, login.Email));

            return
                new ServiceResult<LoginResponse>(
                    new LoginResponse
                    {
                        Token = SecurityHelper.GenerateAccessToken(identity, login.SecureDataFormat, 8),
                        Id = login.Id
                    });
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="roles">
        /// The roles.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<string> Register(User user, string password, params string[] roles)
        {
            var userExists = this.storage.GetUser(user.UserName) != null;

            if (userExists)
            {
                return new ServiceResult<string>(new UserAlreadyExistsError());
            }

            var createUserResult = this.storage.CreateUser(user, password, roles);

            var code = user.Id;

            return createUserResult.Succeeded
                ? new ServiceResult<string>(code)
                : new ServiceResult<string>(new CreateUserError(createUserResult.Errors));
        }

        /// <summary>
        /// The get user profile.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<User> GetUserProfile(string id)
        {
            var user = this.storage.GetUserById(id);
            if (user != null)
            {
                return new ServiceResult<User>(user);
            }

            return new ServiceResult<User>(new UserNotFoundError());
        }

        /// <summary>
        /// The get user profile.
        /// </summary>
        /// <param name="userLoginInfo">
        /// The user login info.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<User> GetUserProfile(UserLoginInfo userLoginInfo)
        {
            var user = this.storage.GetUser(userLoginInfo);
            if (user != null)
            {
                return new ServiceResult<User>(user);
            }

            return new ServiceResult<User>(new UserNotFoundError());
        }

        /// <summary>
        /// The get user profile.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<User> GetUserProfile(string email, string password)
        {
            var user = this.storage.GetUser(email, password);
            if (user != null)
            {
                return new ServiceResult<User>(user);
            }

            return new ServiceResult<User>(new UserNotFoundError());
        }

        /// <summary>
        /// The remove login.
        /// </summary>
        /// <param name="getUserId">
        /// The get user id.
        /// </param>
        /// <param name="loginInfo">
        /// The login info.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> RemoveLogin(string getUserId, UserLoginInfo loginInfo)
        {
            var removeLoginResult = this.storage.RemoveLogin(getUserId, loginInfo);
            return removeLoginResult.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new RemoveLoginError(removeLoginResult.Errors));
        }

        /// <summary>
        /// The create identity.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="applicationCookie">
        /// The application cookie.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<ClaimsIdentity> CreateIdentity(User user, string applicationCookie)
        {
            return new ServiceResult<ClaimsIdentity>(this.storage.CreateIdentity(user, applicationCookie));
        }

        /// <summary>
        /// The add login.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> AddLogin(string id, UserLoginInfo login)
        {
            var result = this.storage.AddLogin(id, login);
            return result.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new AddLoginError(result.Errors));
        }

        /// <summary>
        /// The change password async.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="oldPassword">
        /// The old password.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="Task{T}"/>.
        /// </returns>
        public async Task<ServiceResult<bool>> ChangePasswordAsync(
            string userId, string oldPassword, string newPassword)
        {
            var identityUser = this.storage.GetUserById(userId);

            if (identityUser == null)
            {
                return new ServiceResult<bool>(new UserNotFoundError());
            }

            var result = await this.storage.ChangePasswordAsync(userId, oldPassword, newPassword);
            return result.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new ChangePasswordError(result.Errors));
        }

        /// <summary>
        /// The add password async.
        /// </summary>
        /// <param name="getUserId">
        /// The get user id.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="Task{T}"/>.
        /// </returns>
        public async Task<ServiceResult<bool>> AddPasswordAsync(string getUserId, string newPassword)
        {
            var result = await this.storage.AddPasswordAsync(getUserId, newPassword);
            return result.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new AddPasswordError(result.Errors));
        }

        /// <summary>
        /// The get logins.
        /// </summary>
        /// <param name="getUserId">
        /// The get user id.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<IEnumerable<UserLoginInfo>> GetLogins(string getUserId)
        {
            var result = this.storage.GetLogins(getUserId);
            return new ServiceResult<IEnumerable<UserLoginInfo>>(result);
        }

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> UpdateUser(User user)
        {
            var result = this.storage.Update(user);
            return result.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new UpdateUserError(result.Errors));
        }

        /// <summary>
        /// The update re login required.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> UpdateReLoginRequired(User user)
        {
            var result = this.storage.UpdateReLoginRequired(user);
            return result.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new UpdateUserError(result.Errors));
        }

        /// <summary>
        /// The add user to roles.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="roles">
        /// The roles.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> AddUserToRoles(string id, params string[] roles)
        {
            var result = this.storage.AddUserToRoles(id, roles);
            return result.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new AddUserToRolesError(result.Errors));
        }

        /// <summary>
        /// The remove user from roles.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="roles">
        /// The roles.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> RemoveUserFromRoles(string id, params string[] roles)
        {
            var result = this.storage.RemoveUserFromRoles(id, roles);
            return result.Succeeded
                ? new ServiceResult<bool>(true)
                : new ServiceResult<bool>(new RemoveUserFromRolesError(result.Errors));
        }

        /// <summary>
        /// The remove profile.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> RemoveProfile(string id)
        {
            var user = this.storage.UserManager.FindById(id);

            if (user == null)
            {
                return new ServiceResult<bool>(false);
            }

            var result = this.storage.UserManager.Delete(user);

            return new ServiceResult<bool>(result.Succeeded);
        }

        /// <summary>
        /// The get roles.
        /// </summary>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<List<IdentityRole>> GetRoles()
        {
            var roles = this.storage.GetRoles().Select(e => new IdentityRole { Id = e.Key, Name = e.Value }).ToList();
            return new ServiceResult<List<IdentityRole>>(roles);
        }

        /// <summary>
        /// The get users.
        /// </summary>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<List<User>> GetUsers()
        {
            var result = this.storage.GetUsers().ToList();
            return new ServiceResult<List<User>>(result);
        }

        /// <summary>
        /// The generate password.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GeneratePassword()
        {
            return this.storage.GeneratePassword();
        }

        /// <summary>
        /// The update password.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<bool> UpdatePassword(string userId, string newPassword)
        {
            var identityUser = this.storage.GetUserById(userId);

            if (identityUser == null)
            {
                return new ServiceResult<bool>(new UserNotFoundError());
            }

            var helper = new SecurityHelper(this.storage.UserManager);
            identityUser.PasswordHash = helper.HashPassword(newPassword);
            identityUser.PlainPassword = newPassword;

            var updateResult = this.storage.Update(identityUser);

            return updateResult.Succeeded ? new ServiceResult<bool>(true) : new ServiceResult<bool>(new ChangePasswordError(updateResult.Errors));
        }

        /// <summary>
        /// The generate forgot password token.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        public ServiceResult<ForgetResponse> GenerateForgotPasswordToken(string email)
        {
            var user = this.storage.GetUser(email);

            if (user == null)
            {
                return new ServiceResult<ForgetResponse>(new UserNotFoundError());
            }

            var token = SecurityHelper.GenerateRandomToken();
            user.PasswordResetToken = token;

            var updateResult = this.storage.Update(user);
            return updateResult.Succeeded
                ? new ServiceResult<ForgetResponse>(
                    new ForgetResponse
                    {
                        Result = true,
                        Token = token,
                        Name = $"{user.FirstName} {user.LastName}"
                    })
                : new ServiceResult<ForgetResponse>(new UpdateUserError(updateResult.Errors));
        }

        /// <summary>
        /// The is in role async.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="System.Threading.Tasks.Task"/>.
        /// </returns>
        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var result = false;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(role))
            {
                result = await this.storage.IsInRoleAsync(userId, role);
            }

            return result;
        }

        /// <inheritdoc />
        public ServiceResult<List<IdentityRole>> GetUserRoles(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new ServiceResult<List<IdentityRole>>(new ArgumentNullError());
            }

            var user = this.storage.GetUserById(userId);

            if (user == null)
            {
                return new ServiceResult<List<IdentityRole>>(new NotFoundError());
            }

            var userRoleNames = this.storage.GetUserRoles(userId);
            var result = this.storage.GetRoles()
                .Where(x => userRoleNames.Contains(x.Value))
                .Select(x => new IdentityRole { Id = x.Key, Name = x.Value })
                .ToList();
            return new ServiceResult<List<IdentityRole>>(result);
        }
    }
}