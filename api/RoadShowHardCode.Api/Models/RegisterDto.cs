namespace RoadShowHardCode.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using Ninject.Activation;

    using RoadShowHardCode.Models;

    /// <summary>
    /// The register data transfer object.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [Required]
        public UserType Type { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first level.
        /// </summary>
        [Required]
        public string FirstLevel { get; set; }

        /// <summary>
        /// Gets or sets the second level.
        /// </summary>
        [Required]
        public string SecondLevel { get; set; }
    }
}