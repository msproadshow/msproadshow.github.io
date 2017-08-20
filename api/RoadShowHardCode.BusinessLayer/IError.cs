namespace RoadShowHardCode.BusinessLayer
{
    using System.Collections.Generic;

    /// <summary>
    /// The Error interface.
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// Gets the code.
        /// </summary>
        int ErrorCode { get; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        IEnumerable<string> Errors { get; }
    }
}