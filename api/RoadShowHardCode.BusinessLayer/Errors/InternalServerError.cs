namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The internal server error.
    /// </summary>
    public class InternalServerError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public InternalServerError(IEnumerable<string> errors)
        {
            this.ErrorCode = ErrorCodes.InternalServerError;
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