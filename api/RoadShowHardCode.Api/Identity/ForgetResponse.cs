namespace RoadShowHardCode.Api.Identity
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The forget response.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here.")]
    public class ForgetResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether result.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}