namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The user already exists error.
    /// </summary>
    public class UserAlreadyExistsError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsError"/> class.
        /// </summary>
        public UserAlreadyExistsError()
        {
            this.ErrorCode = ErrorCodes.UserAlreadyExists;
            this.Errors = new List<string> { "User already exists" };
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