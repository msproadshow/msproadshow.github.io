namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The argument error.
    /// </summary>
    public class ArgumentError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentError"/> class.
        /// </summary>
        public ArgumentError()
        {
            this.ErrorCode = ErrorCodes.Argument;
            this.Errors = new List<string> { "Argument" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public ArgumentError(IEnumerable<string> errors)
        {
            this.ErrorCode = ErrorCodes.Argument;
            this.Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentError"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ArgumentError(string message)
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