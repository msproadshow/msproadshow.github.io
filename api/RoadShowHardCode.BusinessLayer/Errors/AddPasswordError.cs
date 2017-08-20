namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The add password error.
    /// </summary>
    public class AddPasswordError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPasswordError"/> class.
        /// </summary>
        public AddPasswordError()
        {
            this.ErrorCode = ErrorCodes.AddPassword;
            this.Errors = new List<string> { "Add Password" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPasswordError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public AddPasswordError(IEnumerable<string> errors)
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
