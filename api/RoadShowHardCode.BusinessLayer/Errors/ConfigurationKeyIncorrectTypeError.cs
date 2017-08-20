namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// Configuration key not found error
    /// </summary>
    public class ConfigurationKeyIncorrectTypeError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationKeyIncorrectTypeError"/> class.
        /// </summary>
        public ConfigurationKeyIncorrectTypeError()
        {
            this.ErrorCode = ErrorCodes.ConfigurationKeyIncorrectType;
            this.Errors = new List<string> { "Configuration Key Incorrect Type" };
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
