namespace RoadShowHardCode.BusinessLayer.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// The invalid password reset token error.
    /// </summary>
    public class InvalidPasswordResetTokenError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordResetTokenError"/> class.
        /// </summary>
        public InvalidPasswordResetTokenError()
        {
            this.ErrorCode = ErrorCodes.InvalidPasswordResetToken;
            this.Errors = new List<string> { "Invalid Password Reset Token" };
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