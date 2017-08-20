namespace RoadShowHardCode.Api.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using RoadShowHardCode.Data.Context;
    using RoadShowHardCode.Models;

    /// <summary>
    /// The asp net identity storage.
    /// </summary>
    public class AspNetIdentityStorage : IIdentityStorage
    {
        /// <summary>
        /// The role manager.
        /// </summary>
        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes static members of the <see cref="AspNetIdentityStorage"/> class.
        /// </summary>
        static AspNetIdentityStorage()
        {
            UserManagerFactory = () =>
                {
                    var um = new UserManager<User>(new UserStore<User>(new DatabaseContext()));
                    return um;
                };

            RoleManagerFactory = () => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DatabaseContext()));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetIdentityStorage"/> class.
        /// </summary>
        public AspNetIdentityStorage()
        {
            this.userManager = UserManagerFactory();
            this.roleManager = RoleManagerFactory();
            this.userManager.UserValidator = new UserValidator<User>(this.userManager)
            {
                AllowOnlyAlphanumericUserNames
                                                         = false
            };

            this.userManager.PasswordValidator = new PasswordValidator
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireUppercase = true,
                RequireLowercase = true,
                RequireNonLetterOrDigit = true
            };
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public UserManager<User> UserManager => this.userManager;

        /// <summary>
        /// Gets the role manager factory.
        /// </summary>
        private static Func<RoleManager<IdentityRole>> RoleManagerFactory { get; }

        /// <summary>
        /// Gets the user manager factory.
        /// </summary>
        private static Func<UserManager<User>> UserManagerFactory { get; }

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User GetUser(string email) => this.userManager.FindByName(email);

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User GetUser(string email, string password) => this.userManager.Find(email, password);

        /// <summary>
        /// The create identity.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="ClaimsIdentity"/>.
        /// </returns>
        public ClaimsIdentity CreateIdentity(User user, string type) => this.userManager.CreateIdentity(user, type);

        /// <summary>
        /// The create user.
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
        /// The <see cref="IdentityResult"/>.
        /// </returns>
        public IdentityResult CreateUser(User user, string password, params string[] roles)
        {
            var identityUser = user;
            var result = this.userManager.Create(identityUser, password);
            if (!result.Succeeded)
            {
                return result;
            }

            result = this.AddUserToRoles(identityUser.Id, roles);
            user.Id = identityUser.Id;

            return result;
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
        /// The <see cref="IdentityResult"/>.
        /// </returns>
        public IdentityResult AddUserToRoles(string id, string[] roles)
        {
            foreach (var role in roles)
            {
                var result = this.userManager.AddToRole(id, role);
                if (!result.Succeeded)
                {
                    return result;
                }
            }

            return IdentityResult.Success;
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
        /// The <see cref="IdentityResult"/>.
        /// </returns>
        public IdentityResult RemoveUserFromRoles(string id, string[] roles)
        {
            foreach (var matrixRole in roles)
            {
                var result = this.userManager.RemoveFromRole(id, matrixRole);
                if (!result.Succeeded)
                {
                    return result;
                }
            }

            return IdentityResult.Success;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="IdentityResult"/>.
        /// </returns>
        public IdentityResult Update(User user)
        {
            var identityUser = this.userManager.FindById(user.Id);

            identityUser.PasswordHash = user.PasswordHash;
            identityUser.PlainPassword = user.PlainPassword;
            identityUser.PasswordResetToken = user.PasswordResetToken;

            return this.userManager.Update(identityUser);
        }

        /// <summary>
        /// The update re login required.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="IdentityResult"/>.
        /// </returns>
        public IdentityResult UpdateReLoginRequired(User user)
        {
            var identityUser = this.userManager.FindById(user.Id);

            // identityUser.IsReLoginRequired = user.IsReLoginRequired;
            return this.userManager.Update(identityUser);
        }

        /// <summary>
        /// The get user by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User GetUserById(string id) => this.userManager.FindById(id);

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="userLoginInfo">
        /// The user login info.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User GetUser(UserLoginInfo userLoginInfo) => this.userManager.Find(userLoginInfo);

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
        /// The <see cref="IdentityResult"/>.
        /// </returns>
        public IdentityResult RemoveLogin(string getUserId, UserLoginInfo loginInfo) => this.userManager.RemoveLogin(getUserId, loginInfo);

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
        /// The <see cref="IdentityResult"/>.
        /// </returns>
        public IdentityResult AddLogin(string id, UserLoginInfo login) => this.userManager.AddLogin(id, login);

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
        public async Task<IdentityResult> AddPasswordAsync(string getUserId, string newPassword) => await this.userManager.AddPasswordAsync(getUserId, newPassword);

        /// <summary>
        /// The get logins.
        /// </summary>
        /// <param name="getUserId">
        /// The get user id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<UserLoginInfo> GetLogins(string getUserId) => this.userManager.GetLogins(getUserId);

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
        public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
        }

        /// <summary>
        /// The generate password.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GeneratePassword()
        {
            const string Specials = "!@#$%^&*()";

            var randomDigit = DateTime.UtcNow.Millisecond.ToString(CultureInfo.InvariantCulture)[0];
            var ch =
                Convert.ToChar(Convert.ToInt32(Math.Floor((26 * new Random().NextDouble()) + 65)))
                       .ToString(CultureInfo.InvariantCulture);

            return Guid.NewGuid().ToString().Substring(0, 6) + randomDigit + ch.ToUpper() + Specials[new Random().Next(9)];
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
        /// The <see cref="Task{T}"/>.
        /// </returns>
        public async Task<bool> IsInRoleAsync(string userId, string role) => await this.userManager.IsInRoleAsync(userId, role);

        /// <summary>
        /// The get roles.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary{T,T}"/>.
        /// </returns>
        public Dictionary<string, string> GetRoles()
        {
            var roles = this.roleManager.Roles.ToList();
            var result = new Dictionary<string, string>();
            roles.ForEach(r => result.Add(r.Id, r.Name));
            return result;
        }

        /// <summary>
        /// The get users.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/> with <see cref="User"/> as <c>T</c> parameter.
        /// </returns>
        public IEnumerable<User> GetUsers()
        {
            if (this.userManager.Users != null)
            {
                var users = this.userManager.Users;
                return users;
            }

            return Enumerable.Empty<User>();
        }

        /// <summary>
        /// The get user roles.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/> with <see cref="string"/> as <c>T</c> parameter.
        /// </returns>
        public IList<string> GetUserRoles(string userId) => this.UserManager.GetRoles(userId);
    }
}