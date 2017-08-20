namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The unknown error.
    /// </summary>
    public class UnknownError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownError"/> class.
        /// </summary>
        public UnknownError()
        {
            this.ErrorCode = ErrorCodes.Unknown;
            this.Errors = new List<string> { "Unknown error" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public UnknownError(IEnumerable<string> errors)
        {
            this.ErrorCode = ErrorCodes.Unknown;
            this.Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownError"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public UnknownError(string message)
            : this(new List<string> { message })
        {
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