namespace Arvi.Omt.Api.Handler
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Common.Logging;

    using RoadShowHardCode.Api.Handler;

    /// <summary>
    /// The message logging handler.
    /// </summary>
    public class MessageLoggingHandler : MessageHandler
    {
        /// <summary>
        /// The log row.
        /// </summary>
        private const string LogRow =
@"Application = '{0}'; User = '{1}'; Machine = '{2}'; RequestIpAddress = '{3}'; RequestContentType = '{4}'; RequestContentBody = '{5}'; RequestUri = '{6}'; RequestMethod = '{7}'; RequestRouteTemplate = '{8}'; RequestRouteData = '{9}'; RequestHeaders = '{10}'; RequestTimestamp = '{11}'; ResponseContentType = '{12}'; ResponseContentBody = '{13}'; ResponseStatusCode = '{14}'; ResponseHeaders = '{15}'; ResponseTimestamp = '{16}'";

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLoggingHandler"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public MessageLoggingHandler(ILog logger)
        {
            this.logger = logger;
        }

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
        protected override async Task IncomingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            await Task.Run(
                () => this.logger.Info(
                    $"{correlationId} - Request: {requestInfo}\r\n{Encoding.UTF8.GetString(message)}"));
        }

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
        protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            await Task.Run(
                () => this.logger.Info(
                    $"{correlationId} - Response: {requestInfo}\r\n{Encoding.UTF8.GetString(message)}"));
        }

        /// <summary>
        /// The full message async.
        /// </summary>
        /// <param name="apiLogEntity">
        /// The ApiLogEntity info.
        /// </param>
        /// <param name="requestInfo">
        /// The request info.
        /// </param>
        /// <returns></returns>
        protected override async Task MessageAsync(ApiLogEntity apiLogEntity, string requestInfo)
        {
            if (requestInfo.Contains("analytics/collect"))
            {
                await Task.Run(
                    () => this.logger.Info(
                        string.Format(
                            LogRow,
                            apiLogEntity.Application,
                            apiLogEntity.User,
                            apiLogEntity.Machine,
                            apiLogEntity.RequestIpAddress,
                            apiLogEntity.RequestContentType,
                            apiLogEntity.RequestContentBody,
                            apiLogEntity.RequestUri,
                            apiLogEntity.RequestMethod,
                            apiLogEntity.RequestRouteTemplate,
                            apiLogEntity.RequestRouteData,
                            apiLogEntity.RequestHeaders,
                            apiLogEntity.RequestTimestamp,
                            apiLogEntity.ResponseContentType,
                            apiLogEntity.ResponseContentBody,
                            apiLogEntity.ResponseStatusCode,
                            apiLogEntity.ResponseHeaders,
                            apiLogEntity.ResponseTimestamp)));
            }
        }

        private static IEnumerable<string> IgnorList()
        {
            var ignorListSettings = ConfigurationManager.AppSettings["MessageLoggingHandler.IgnorList"] ?? string.Empty;
            var ignorList = ignorListSettings.Split(';');
            ignorList = ignorList.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            return ignorList;
        }
    }
}