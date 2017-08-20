namespace RoadShowHardCode.Api.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.ServiceModel.Channels;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Routing;

    using Arvi.Omt.Api.Handler;

    using Microsoft.Owin;

    using Newtonsoft.Json;

    /// <summary>
    /// The message handler.
    /// </summary>
    public abstract class MessageHandler : DelegatingHandler
    {
        /// <summary>
        /// The http context.
        /// </summary>
        private const string HttpContext = "MS_HttpContext";

        /// <summary>
        /// The remote endpoint message.
        /// </summary>
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        /// <summary>
        /// The owin context.
        /// </summary>
        private const string OwinContext = "MS_OwinContext";

        /// <summary>
        /// The incoming message async.
        /// </summary>
        /// <param name="correlationId">
        /// The correlation id.
        /// </param>
        /// <param name="requestInfo">
        /// The request info.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected abstract Task IncomingMessageAsync(string correlationId, string requestInfo, byte[] message);

        /// <summary>
        /// The outgoing message async.
        /// </summary>
        /// <param name="correlationId">
        /// The correlation id.
        /// </param>
        /// <param name="requestInfo">
        /// The request info.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected abstract Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message);

        /// <summary>
        /// The full message async.
        /// </summary>
        /// <param name="apiLogEntity">
        /// The ApiLogEntity info.
        /// </param>
        /// <param name="requestInfo">
        /// The request info.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected abstract Task MessageAsync(ApiLogEntity apiLogEntity, string requestInfo);

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
            var corrId = $"{DateTime.Now.Ticks}{Thread.CurrentThread.ManagedThreadId}";
            var requestInfo = $"{request.Method} {request.RequestUri}";

            var apiLogEntity = CreateApiLogEntryWithRequestData(request);

            await request.Content.ReadAsStringAsync().ContinueWith(
                task =>
                    {
                        apiLogEntity.RequestContentBody = task.Result;
                    },
                cancellationToken);

            var requestMessage = await request.Content.ReadAsByteArrayAsync();

            await this.IncomingMessageAsync(corrId, requestInfo, requestMessage);

            var response = await base.SendAsync(request, cancellationToken);
            apiLogEntity.ResponseStatusCode = (int)response.StatusCode;
            apiLogEntity.ResponseTimestamp = DateTime.UtcNow;

            byte[] responseMessage;

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                responseMessage = await response.Content.ReadAsByteArrayAsync();

                apiLogEntity.ResponseContentBody = response.Content.ReadAsStringAsync().Result;
                apiLogEntity.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                apiLogEntity.ResponseHeaders = SerializeHeaders(response.Content.Headers);
            }
            else
            {
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);
            }

            await this.OutgoingMessageAsync(corrId, requestInfo, responseMessage);

            await this.MessageAsync(apiLogEntity, requestInfo);

            return response;
        }

        /// <summary>
        /// The create api log entry with request data.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ApiLogEntity"/>.
        /// </returns>
        private static ApiLogEntity CreateApiLogEntryWithRequestData(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();

            var ret = new ApiLogEntity
            {
                Application = "RoadShowHardCode.Api",
                Machine = Environment.MachineName,
                RequestRouteTemplate = routeData != null ? routeData.Route.RouteTemplate : string.Empty,
                RequestRouteData = SerializeRouteData(routeData),
                RequestMethod = request.Method.Method,
                RequestHeaders = SerializeHeaders(request.Headers),
                RequestTimestamp = DateTime.UtcNow,
                RequestUri = request.RequestUri.ToString()
            };

            // Web-hosting
            if (request.Properties.ContainsKey(HttpContext))
            {
                var ctx =
                    (HttpContextWrapper)request.Properties[HttpContext];
                if (ctx != null)
                {
                    ret.User = ctx.User.Identity.Name;
                    ret.RequestIpAddress = ctx.Request.UserHostAddress;
                    ret.RequestContentType = ctx.Request.ContentType;
                }
            }

            // Self-hosting using Owin
            if (request.Properties.ContainsKey(OwinContext))
            {
                var owinContext = (OwinContext)request.Properties[OwinContext];
                if (owinContext != null)
                {
                    ret.User = owinContext.Authentication.User.Identity.Name;
                    ret.RequestIpAddress = owinContext.Request.RemoteIpAddress;
                    ret.RequestContentType = owinContext.Request.ContentType;
                }
            }

            // Self-hosting
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                var remoteEndpoint =
                    (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessage];

                if (remoteEndpoint != null)
                {
                    ret.RequestIpAddress = remoteEndpoint.Address;
                }
            }

            return ret;
        }

        /// <summary>
        /// The serialize route data.
        /// </summary>
        /// <param name="routeData">
        /// The route data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string SerializeRouteData(IHttpRouteData routeData)
        {
            return JsonConvert.SerializeObject(routeData, Formatting.Indented);
        }

        /// <summary>
        /// The serialize headers.
        /// </summary>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = string.Empty;
                    foreach (var value in item.Value)
                    {
                        header += value + " ";
                    }

                    header = header.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, header);
                }
            }

            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }
    }
}