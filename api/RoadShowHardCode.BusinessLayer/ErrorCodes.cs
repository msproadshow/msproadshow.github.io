namespace RoadShowHardCode.BusinessLayer
{
    /// <summary>
    /// The error codes.
    /// </summary>
    public class ErrorCodes
    {
        /// <summary>
        /// Gets the invalid password error code.
        /// </summary>
        public static int InvalidPassword => 10001;

        /// <summary>
        /// Gets the invalid password reset token error code.
        /// </summary>
        public static int InvalidPasswordResetToken => 10002;

        /// <summary>
        /// Gets the argument null error code.
        /// </summary>
        public static int ArgumentNull => 10003;

        /// <summary>
        /// Get the user not found error code.
        /// </summary>
        public static int UserNotFound => 10004;

        /// <summary>
        /// Gets the argument error code.
        /// </summary>
        public static int Argument => 10005;

        /// <summary>
        /// Gets the user already exists error code.
        /// </summary>
        public static int UserAlreadyExists => 10006;

        /// <summary>
        /// Gets the internal server error.
        /// </summary>
        public static int InternalServerError => 10007;

        /// <summary>
        ///  Gets the account is not activated.
        /// </summary>
        public static int AccountIsNotActivated => 10008;

        /// <summary>
        /// Gets the change password.
        /// </summary>
        public static int ChangePassword => 10009;

        /// <summary>
        /// Gets the update user.
        /// </summary>
        public static int UpdateUser => 10010;

        /// <summary>
        /// Gets the create user error.
        /// </summary>
        public static int CreateUser => 10011;

        /// <summary>
        /// Gets the remove login error code.
        /// </summary>
        public static int RemoveLogin => 10012;

        /// <summary>
        /// Gets the add login error code.
        /// </summary>
        public static int AddLogin => 10013;

        /// <summary>
        /// Gets the add password error code.
        /// </summary>
        public static int AddPassword => 10014;

        /// <summary>
        /// Gets the add user to roles error code.
        /// </summary>
        public static int AddUserToRoles => 10015;

        /// <summary>
        /// Gets the remove user from roles error code.
        /// </summary>
        public static int RemoveUserFromRoles => 10016;

        /// <summary>
        /// The file not found error code.
        /// </summary>
        public static int FileNotFoundError => 10017;

        /// <summary>
        /// The not found error code.
        /// </summary>
        public static int NotFound => 10018;

        /// <summary>
        /// The unknown error code.
        /// </summary>
        public static int Unknown => 10019;

        /// <summary>
        /// The configuration key incorrect type.
        /// </summary>
        public static int ConfigurationKeyIncorrectType => 10020;

        /// <summary>
        /// The configuration key not found.
        /// </summary>
        public static int ConfigurationKeyNotFound => 10021;
    }
}
