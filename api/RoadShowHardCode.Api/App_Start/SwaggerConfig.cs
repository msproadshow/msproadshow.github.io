[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RoadShowHardCode.Api.SwaggerConfig), "Register")]

namespace RoadShowHardCode.Api
{
    using Swashbuckle.Application;

    /// <summary>
    /// The swagger config.
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// The register.
        /// </summary>
        public static void Register()
        {
            StartupConfig.Config.EnableSwagger(
                c =>
                    {
                        c.SingleApiVersion("v1", "RoadShowHardCode.Api");

                        c.IncludeXmlComments(
                            System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/RoadShowHardCode.Api.xml"));
                    }).EnableSwaggerUi();

        }
    }
}
