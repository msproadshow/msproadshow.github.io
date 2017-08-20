namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The remove user from roles error.
    /// </summary>
    public class RemoveUserFromRolesError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveUserFromRolesError"/> class.
        /// </summary>
        public RemoveUserFromRolesError()
        {
            this.ErrorCode = ErrorCodes.RemoveUserFromRoles;
            this.Errors = new List<string> { "Remove User From Roles" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveUserFromRolesError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public RemoveUserFromRolesError(IEnumerable<string> errors)
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
