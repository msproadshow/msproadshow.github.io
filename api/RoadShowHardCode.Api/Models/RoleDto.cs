namespace RoadShowHardCode.Api.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Data transfer object for user role
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDto"/> class.
        /// </summary>
        public RoleDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDto"/> class.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        public RoleDto(IdentityRole role)
        {
            this.RoleId = role.Id;
            this.RoleName = role.Name;
        }

        /// <summary>
        /// Gets or sets Role id.
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Gets or sets Role name.
        /// </summary>
        public string RoleName { get; set; }
    }
}