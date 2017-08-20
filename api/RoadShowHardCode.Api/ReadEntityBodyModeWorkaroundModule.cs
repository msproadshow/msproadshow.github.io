namespace RoadShowHardCode.Api
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Web;

    // fix “This method or property is not supported after HttpRequest.GetBufferlessInputStream has been invoked.”
    // to avoid  buffer less input stream leverage

    /// <summary>
    /// The read entity body mode workaround module.
    /// </summary>
    public class ReadEntityBodyModeWorkaroundModule : IHttpModule
    {
        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// The initialization.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.context_BeginRequest;
        }

        /// <summary>
        /// The context_ begin request.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
        private void context_BeginRequest(object sender, EventArgs e)
        {
            // This will force the HttpContext.Request.ReadEntityBody to be "Classic" and will ensure compatibility..
            var httpApplication = sender as HttpApplication;
            if (httpApplication != null)
            {
                var stream = httpApplication.Request.InputStream;
            }
        }
    }
}