namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The invalid password error.
    /// </summary>
    public class InvalidPasswordError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordError"/> class.
        /// </summary>
        public InvalidPasswordError()
        {
            this.ErrorCode = ErrorCodes.InvalidPassword;
            this.Errors = new List<string> { "Invalid Password" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public InvalidPasswordError(IEnumerable<string> errors)
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