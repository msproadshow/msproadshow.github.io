namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The configuration key not found error.
    /// </summary>
    public class ConfigurationKeyNotFoundError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationKeyNotFoundError"/> class.
        /// </summary>
        public ConfigurationKeyNotFoundError()
        {
            this.ErrorCode = ErrorCodes.ConfigurationKeyNotFound;
            this.Errors = new List<string> { "Configuration Key Not Found" };
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
