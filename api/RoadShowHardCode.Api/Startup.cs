using RoadShowHardCode.Api;

[assembly: Microsoft.Owin.OwinStartup(typeof(Startup))]

namespace RoadShowHardCode.Api
{
    using System.Web.Routing;

    using Owin;

    /// <summary>
    /// The startup.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);

            WebApiConfig.Register(StartupConfig.Config);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            app.UseWebApi(StartupConfig.Config);
        }
    }
}