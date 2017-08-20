namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The update user error.
    /// </summary>
    public class UpdateUserError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserError"/> class.
        /// </summary>
        public UpdateUserError()
        {
            this.ErrorCode = ErrorCodes.UpdateUser;
            this.Errors = new List<string> { "Update user" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public UpdateUserError(IEnumerable<string> errors)
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
