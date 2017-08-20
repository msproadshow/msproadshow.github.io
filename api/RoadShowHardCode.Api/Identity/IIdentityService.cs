namespace RoadShowHardCode.Api.Identity
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using RoadShowHardCode.Models;

    /// <summary>
    /// The IdentityService interface.
    /// </summary>
    public interface IIdentityService
    {
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
        Task<ServiceResult<bool>> ChangePasswordAsync(User user, string newPassword);

        /// <summary>
        /// The generate forgot password token.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<ForgetResponse> GenerateForgotPasswordToken(string email);

        /// <summary>
        /// The authorize.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<LoginResponse> OAuthAuthorize(LoginInfo login);

        /// <summary>
        /// The re issue credentials.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<LoginResponse> ReIssueCredentials(LoginInfo login);

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
        ServiceResult<string> Register(User user, string password, params string[] roles);

        /// <summary>
        /// The get user profile.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<User> GetUserProfile(string id);

        /// <summary>
        /// The get user profile.
        /// </summary>
        /// <param name="userLoginInfo">
        /// The user login info.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<User> GetUserProfile(UserLoginInfo userLoginInfo);

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
        ServiceResult<User> GetUserProfile(string email, string password);

        /// <summary>
        /// The remove login.
        /// </summary>
        /// <param name="getUserId">
        /// The get user id.
        /// </param>
        /// <param name="userLoginInfo">
        /// The user login info.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<bool> RemoveLogin(string getUserId, UserLoginInfo userLoginInfo);

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
        ServiceResult<ClaimsIdentity> CreateIdentity(User user, string applicationCookie);

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
        ServiceResult<bool> AddLogin(string id, UserLoginInfo login);

        /// <summary>
        /// The change password.
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
        Task<ServiceResult<bool>> ChangePasswordAsync(string userId, string oldPassword, string newPassword);

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
        ServiceResult<bool> ChangePassword(string userLogin, string newPassword);

        /// <summary>
        /// The add password.
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
        Task<ServiceResult<bool>> AddPasswordAsync(string getUserId, string newPassword);

        /// <summary>
        /// The get logins.
        /// </summary>
        /// <param name="getUserId">
        /// The get user id.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<IEnumerable<UserLoginInfo>> GetLogins(string getUserId);

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<bool> UpdateUser(User user);

        /// <summary>
        /// The update re login required.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see>
        ///     <cref>ServiceResult</cref>
        /// </see>
        /// .
        /// </returns>
        ServiceResult<bool> UpdateReLoginRequired(User user);

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
        ServiceResult<bool> AddUserToRoles(string id, params string[] roles);

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
        ServiceResult<bool> RemoveUserFromRoles(string id, params string[] roles);

        /// <summary>
        /// The remove profile.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<bool> RemoveProfile(string id);

        /// <summary>
        /// The get roles.
        /// </summary>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<List<IdentityRole>> GetRoles();

        /// <summary>
        /// The get users.
        /// </summary>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<List<User>> GetUsers();

        /// <summary>
        /// The get user roles.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResult{T}"/>.
        /// </returns>
        ServiceResult<List<IdentityRole>> GetUserRoles(string userId);

        /// <summary>
        /// The generate password.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GeneratePassword();

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
        ServiceResult<bool> UpdatePassword(string userId, string newPassword);

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
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> IsInRoleAsync(string userId, string role);
    }
}