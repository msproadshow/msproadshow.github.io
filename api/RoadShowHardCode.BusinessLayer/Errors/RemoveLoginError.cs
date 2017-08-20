namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The remove login error.
    /// </summary>
    public class RemoveLoginError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveLoginError"/> class.
        /// </summary>
        public RemoveLoginError()
        {
            this.ErrorCode = ErrorCodes.RemoveLogin;
            this.Errors = new List<string> { "Remove Login" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveLoginError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public RemoveLoginError(IEnumerable<string> errors)
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
