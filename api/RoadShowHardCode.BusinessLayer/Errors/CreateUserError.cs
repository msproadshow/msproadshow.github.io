namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The create user error.
    /// </summary>
    public class CreateUserError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserError"/> class.
        /// </summary>
        public CreateUserError()
        {
            this.ErrorCode = ErrorCodes.CreateUser;
            this.Errors = new List<string> { "Create User" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public CreateUserError(IEnumerable<string> errors)
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
