namespace RoadShowHardCode.Api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Common.Logging;

    using Microsoft.AspNet.Identity;

    using RoadShowHardCode.Api;
    using RoadShowHardCode.Api.Identity;
    using RoadShowHardCode.Api.Models;
    using RoadShowHardCode.BusinessLayer.Errors;
    using RoadShowHardCode.Models;

    /// <summary>
    /// Account handling
    /// </summary>
    [RoutePrefix("Authorize")]
    public class AccountController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="service">
        /// The service.
        /// </param>
        public AccountController(ILog logger, IIdentityService service)
            : base(logger)
        {
            this.IdentityService = service;
        }

        /// <summary>
        /// Gets the identity service.
        /// </summary>
        public IIdentityService IdentityService { get; }

        /// <summary>
        /// The forgot password.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ForgotPassword")]
        public IHttpActionResult ForgotPassword(string email, string language = "en")
        {
            if (email == null)
            {
                return this.Error(new ArgumentNullError());
            }

            var response = this.IdentityService.GenerateForgotPasswordToken(email);

            if (!response.Succeeded)
            {
                return this.Error(response);
            }

            // this.UserMailer.Language = language;
            // this.UserMailer.SendForgotPassword(
            // email, response.Data.Name, response.Data.Token, MailType.ForgotPasswordAgent);
            return this.Ok(response.Data.Token);
        }

        /// <summary>
        /// The get user information.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("userInfo")]
        public IHttpActionResult GetUserInfo()
        {
            var userId = this.User.Identity.GetUserId();
            var userInfoResponse = this.IdentityService.GetUserProfile(userId);
            var userRoleResponse = this.IdentityService.GetUserRoles(userId);

            if (!userInfoResponse.Succeeded)
            {
                return this.Error(userInfoResponse);
            }

            if (!userRoleResponse.Succeeded)
            {
                return this.Error(userRoleResponse);
            }

            return this.Ok(new UserInfoDto(userInfoResponse.Data, userRoleResponse.Data));
        }

        /// <summary>
        /// The post authorize.
        /// </summary>
        /// <param name="dto">
        /// The data transfer object.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        [Route("")]
        public IHttpActionResult PostAuthorize(LoginDto dto)
        {
            var loginInfo = new LoginInfo
            {
                Email = dto.Email,
                Password = dto.Password,
                AuthenticationType = Startup.OAuthBearerOptions.AuthenticationType,
                SecureDataFormat = Startup.OAuthBearerOptions.AccessTokenFormat,
            };

            var response = this.IdentityService.OAuthAuthorize(loginInfo);

            if (!response.Succeeded)
            {
                return this.Error(response);
            }

            return this.Ok(new AccessTokenDto { Token = response.Data.Token, });
        }

        /// <summary>
        /// The post change password.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AllowAnonymous]
        [Route("ChangePassword")]
        public IHttpActionResult PostChangePassword(string email, string token, string newPassword)
        {
            var user = new User { UserName = email, PasswordResetToken = token };
            var response = this.IdentityService.ChangePasswordAsync(user, newPassword)
                               .Result;

            if (!response.Succeeded)
            {
                return this.Error(response);
            }

            return this.Ok();
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        public IHttpActionResult Register(RegisterDto model)
        {
            if (model == null)
            {
                return this.Error(new ArgumentNullError());
            }

            if (!this.ModelState.IsValid)
            {
                var errors = this.ModelState.Values
                    .SelectMany(e => e.Errors.Select(x => x.ErrorMessage ?? x.Exception.Message)).ToList();
                return this.Error(errors);
            }

            var serviceResult = this.IdentityService.Register(
                new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstLevel = model.FirstLevel,
                    SecondLevel = model.SecondLevel,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                },
                model.Password);
            if (!serviceResult.Succeeded)
            {
                return this.Error(serviceResult.Errors);
            }

            return this.Ok(serviceResult.Data);
        }
    }
}