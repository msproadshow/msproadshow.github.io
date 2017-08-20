namespace RoadShowHardCode.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using RoadShowHardCode.BusinessLayer;

    /// <summary>
    /// Custom Error
    /// </summary>
    public class ApiError : IHttpActionResult
    {
        /// <summary>
        /// The request.
        /// </summary>
        protected readonly HttpRequestMessage Request;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// Constructor
        /// </summary>
        /// <param name="statusCode">
        /// The status Code.
        /// </param>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        public ApiError(HttpStatusCode statusCode, IError error, ApiController controller)
        {
            this.StatusCode = statusCode;
            this.ErrorCode = error.ErrorCode;
            this.ErrorMessage = string.Join(",", error.Errors);
            this.Request = controller?.Request;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The execute async.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.Request.CreateResponse(this.StatusCode, new { this.ErrorCode, this.ErrorMessage });
            return Task.FromResult(response);
        }

        /// <summary>
        /// Execute synchronously
        /// </summary>
        /// <param name="context">
        /// The context
        /// </param>
        public void Execute(HttpActionContext context)
        {
            context.Response = context.Request.CreateResponse(
                this.StatusCode, new { this.ErrorCode, this.ErrorMessage });
        }

        /// <summary>
        /// Execute synchronously
        /// </summary>
        /// <param name="context">
        /// The context
        /// </param>
        public void Execute(HttpActionExecutedContext context)
        {
            context.Response = context.Request.CreateResponse(
                this.StatusCode, new { this.ErrorCode, this.ErrorMessage });
        }
    }
}