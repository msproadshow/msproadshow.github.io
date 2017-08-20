namespace RoadShowHardCode.Api.Handler
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    /// <summary>
    /// The file result.
    /// </summary>
    public class FileResult : IHttpActionResult
    {
        /// <summary>
        /// The file path.
        /// </summary>
        private readonly string filePath;

        /// <summary>
        /// The content type.
        /// </summary>
        private readonly string contentType;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileResult"/> class.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="contentType">
        /// The content type.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Null value in <see cref="filePath"/>
        /// </exception>
        public FileResult(string filePath, string contentType = null)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this.contentType = contentType;
        }

        /// <summary>
        /// The execute async.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(File.OpenRead(MapPath(this.filePath)))
            };

            var localContentType = this.contentType ?? MimeMapping.GetMimeMapping(Path.GetExtension(this.filePath ?? string.Empty));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(localContentType);

            return Task.FromResult(response);
        }

        /// <summary>
        /// The map path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }
    }
}