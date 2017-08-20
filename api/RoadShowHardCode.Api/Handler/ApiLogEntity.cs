using System;

namespace Arvi.Omt.Api.Handler
{
    /// <summary>
    /// Api Log Entity
    /// </summary>
    public class ApiLogEntity
    {
        /// <summary>
        /// The (database) ID for the API log entry.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The application that made the request.
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// The user that made the request.
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// The machine that made the request.
        /// </summary>
        public string Machine { get; set; }
        /// <summary>
        /// The IP address that made the request.
        /// </summary>
        public string RequestIpAddress { get; set; }
        /// <summary>
        /// The request content type.
        /// </summary>
        public string RequestContentType { get; set; }
        /// <summary>
        /// The request content body.
        /// </summary>
        public string RequestContentBody { get; set; }
        /// <summary>
        /// The request URI.
        /// </summary>
        public string RequestUri { get; set; }
        /// <summary>
        /// The request method (GET, POST, etc).
        /// </summary>
        public string RequestMethod { get; set; }
        /// <summary>
        /// The request route template.
        /// </summary>
        public string RequestRouteTemplate { get; set; }
        /// <summary>
        /// The request route data.
        /// </summary>
        public string RequestRouteData { get; set; }
        /// <summary>
        /// The request headers.
        /// </summary>
        public string RequestHeaders { get; set; }
        /// <summary>
        /// The request timestamp.
        /// </summary>
        public DateTime? RequestTimestamp { get; set; }
        /// <summary>
        /// The response content type.
        /// </summary>
        public string ResponseContentType { get; set; }
        /// <summary>
        /// The response content body.
        /// </summary>
        public string ResponseContentBody { get; set; }
        /// <summary>
        /// The response status code.
        /// </summary>
        public int? ResponseStatusCode { get; set; }
        /// <summary>
        /// The response headers.
        /// </summary>
        public string ResponseHeaders { get; set; }
        /// <summary>
        /// The response timestamp.
        /// </summary>
        public DateTime? ResponseTimestamp { get; set; }
    }
}