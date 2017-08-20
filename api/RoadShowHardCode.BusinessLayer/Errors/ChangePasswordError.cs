namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The change password error.
    /// </summary>
    public class ChangePasswordError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordError"/> class.
        /// </summary>
        public ChangePasswordError()
        {
            this.ErrorCode = ErrorCodes.ChangePassword;
            this.Errors = new List<string> { "Change Password" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordError"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public ChangePasswordError(IEnumerable<string> errors)
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
