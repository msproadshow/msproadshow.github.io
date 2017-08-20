namespace RoadShowHardCode.Api.Filters
{
    using System;
    using System.Net;
    using System.Web.Http.Filters;

    using Common.Logging;

    using RoadShowHardCode.BusinessLayer.Errors;

    /// <summary>
    /// The unhandled exception filter.
    /// </summary>
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public UnhandledExceptionFilter(ILog logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                return;
            }

            this.LogException(context.Exception);
            this.CreateHttpError(context, context.Exception);
        }

        /// <summary>
        /// The get http status code.
        /// </summary>
        /// <param name="context">
        /// The context
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        private void CreateHttpError(HttpActionExecutedContext context, Exception exception)
        {
            this.logger.Error("No ErrorCode for exception", exception);
            var error = new ApiError(HttpStatusCode.InternalServerError, new UnknownError("Error occurred"), null);
            error.Execute(context);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        private void LogException(Exception exception)
        {
            try
            {
                this.logger.Error("Unhandled Exception occurred.", exception);
            }
            catch
            {
                // Just suppress all exceptions.
            }
        }
    }
}