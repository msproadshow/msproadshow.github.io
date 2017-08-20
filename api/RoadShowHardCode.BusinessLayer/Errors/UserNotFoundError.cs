namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The user not found error.
    /// </summary>
    public class UserNotFoundError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundError"/> class.
        /// </summary>
        public UserNotFoundError()
        {
            this.ErrorCode = ErrorCodes.UserNotFound;
            this.Errors = new List<string> { "User Not Found" };
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