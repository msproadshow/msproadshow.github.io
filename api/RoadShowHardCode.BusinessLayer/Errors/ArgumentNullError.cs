namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The argument null error.
    /// </summary>
    public class ArgumentNullError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNullError"/> class.
        /// </summary>
        public ArgumentNullError()
        {
            this.ErrorCode = ErrorCodes.ArgumentNull;
            this.Errors = new List<string> { "Argument Null" };
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