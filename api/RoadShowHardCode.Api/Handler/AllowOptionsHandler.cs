namespace RoadShowHardCode.Api.Handler
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The allow options handler.
    /// </summary>
    public class AllowOptionsHandler : DelegatingHandler
    {
        /// <summary>
        /// The send async.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (request.Method == HttpMethod.Options &&
                response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }
    }
}