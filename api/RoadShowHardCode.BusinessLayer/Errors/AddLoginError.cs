namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The add login error.
    /// </summary>
    public class AddLoginError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddLoginError"/> class.
        /// </summary>
        public AddLoginError()
        {
            this.ErrorCode = ErrorCodes.AddLogin;
            this.Errors = new List<string> { "Add login error" };
        }

        public AddLoginError(IEnumerable<string> errors)
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
