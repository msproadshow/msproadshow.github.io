namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The not found error.
    /// </summary>
    public class NotFoundError : IError
    {
        /// <summary>
        /// The not found.
        /// </summary>
        private const string NotFound = "Not Found";

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundError" /> class.
        /// </summary>
        public NotFoundError()
        {
            this.ErrorCode = ErrorCodes.NotFound;
            this.Errors = new List<string> { NotFound };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundError"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public NotFoundError(string message)
            : this()
        {
            this.Errors = new List<string> { NotFound + ' ' + message };
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