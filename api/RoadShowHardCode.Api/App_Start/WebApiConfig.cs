namespace RoadShowHardCode.Api
{
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Arvi.Omt.Api.Handler;

    using Cors.ConfigProfiles;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using RoadShowHardCode.Api.Handler;

    /// <summary>
    /// The web api config.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Web API configuration and services.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(Startup.OAuthBearerOptions.AuthenticationType));

            config.Formatters.Clear();
            JsonMediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            var isoConverter = new IsoDateTimeConverter();
            isoConverter.DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
            jsonSettings.Converters.Add(isoConverter);
            jsonFormatter.SerializerSettings = jsonSettings;
            config.Formatters.Add(jsonFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors(new EnableCorsAttribute("Default"));
            config.MessageHandlers.Add(new AllowOptionsHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}