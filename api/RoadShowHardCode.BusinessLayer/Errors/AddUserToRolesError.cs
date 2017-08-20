namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The add user to roles error.
    /// </summary>
    public class AddUserToRolesError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserToRolesError"/> class.
        /// </summary>
        public AddUserToRolesError()
        {
            this.ErrorCode = ErrorCodes.AddUserToRoles;
            this.Errors = new List<string> { "Add User To Roles" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserToRolesError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public AddUserToRolesError(IEnumerable<string> errors)
            : this()
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        public IEnumerable<string> Errors { get; }
    }
}
