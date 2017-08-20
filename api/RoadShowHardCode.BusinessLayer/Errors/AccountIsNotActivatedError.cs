namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    using RoadShowHardCode.BusinessLayer;

    /// <summary>
    /// The account is not activated error.
    /// </summary>
    public class AccountIsNotActivatedError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountIsNotActivatedError"/> class.
        /// </summary>
        public AccountIsNotActivatedError()
        {
            this.ErrorCode = ErrorCodes.AccountIsNotActivated;
            this.Errors = new List<string> { "Account is not activated error" };
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
