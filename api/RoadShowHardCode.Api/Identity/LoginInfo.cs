namespace RoadShowHardCode.Api.Identity
{
    using Microsoft.Owin.Security;

    /// <summary>
    /// The login info.
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the authentication type.
        /// </summary>
        public string AuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets the secure data format.
        /// </summary>
        public ISecureDataFormat<AuthenticationTicket> SecureDataFormat { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }
    }
}