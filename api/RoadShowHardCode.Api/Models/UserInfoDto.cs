namespace RoadShowHardCode.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using RoadShowHardCode.Models;

    /// <summary>
    /// Information about user
    /// </summary>
    public class UserInfoDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoDto"/> class.
        /// </summary>
        public UserInfoDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoDto"/> class.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="userRoles">
        /// The user roles.
        /// </param>
        public UserInfoDto(User user, List<IdentityRole> userRoles)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Roles = userRoles.Select(x => new RoleDto(x)).ToList();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public List<RoleDto> Roles { get; set; }
    }
}