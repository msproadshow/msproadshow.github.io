namespace RoadShowHardCode.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The user.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public UserType Type { get; set; }

        /// <summary>
        /// Gets or sets the first level.
        /// </summary>
        public string FirstLevel { get; set; }

        /// <summary>
        /// Gets or sets the second level.
        /// </summary>
        public string SecondLevel { get; set; }

        /// <summary>
        /// Gets or sets the plain password.
        /// </summary>
        public string PlainPassword { get; set; }

        /// <inheritdoc />
        public override string UserName
        {
            get => base.UserName;
            set => base.UserName = value;
        }

        /// <summary>
        /// Gets or sets the password reset token.
        /// </summary>
        public string PasswordResetToken { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }
    }
}