namespace RoadShowHardCode.Api
{
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    /// <summary>
    /// The startup.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Initializes static members of the <see cref="Startup" /> class.
        /// </summary>
        static Startup()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        }

        /// <summary>
        /// Gets the o authorization bearer options.
        /// </summary>
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; }

        /// <summary>
        /// The configure authorization.
        /// </summary>
        /// <param name="app">
        /// The application.
        /// </param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}