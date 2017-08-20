namespace RoadShowHardCode.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    using Common.Logging;

    using RoadShowHardCode.BusinessLayer;
    using RoadShowHardCode.BusinessLayer.Errors;

    /// <summary>
    /// The base controller.
    /// </summary>
    public abstract class BaseController : ApiController
    {
        /// <summary>
        /// The errors.
        /// </summary>
        private readonly Dictionary<int, HttpStatusCode> errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        protected BaseController()
        {
            this.errors = new Dictionary<int, HttpStatusCode>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        protected BaseController(ILog logger)
            : this()
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        public ILog Logger { get; }


        /// <summary>
        /// The error.
        /// </summary>
        /// <typeparam name="T">
        /// Any error
        /// </typeparam>
        /// <returns>
        /// The <see cref="ApiError"/>.
        /// </returns>
        protected ApiError Error<T>()
            where T : IError, new()
        {
            var errorInstance = new T();

            return this.Error(errorInstance);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="error">
        /// The error object.
        /// </param>
        /// <returns>
        /// The <see cref="ApiError"/>.
        /// </returns>
        protected ApiError Error(IError error)
        {
            var errorInstance = this.errors.ContainsKey(error.ErrorCode)
                                    ? new ApiError(this.errors[error.ErrorCode], error, this)
                                    : new ApiError(HttpStatusCode.BadRequest, error, this);

            return errorInstance;
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <returns>
        /// The <see cref="ApiError"/>.
        /// </returns>
        protected ApiError Error(IEnumerable<string> errorList) => new ApiError(
            HttpStatusCode.InternalServerError,
            new InternalServerError(errorList),
            this);

        /// <summary>
        /// Check model state and put converted error
        /// </summary>
        /// <returns>
        /// The <see cref="ApiError" />.
        /// </returns>
        protected ApiError GetModelError()
        {
            if (this.ModelState.IsValid)
            {
                return null;
            }

            return new ApiError(
                HttpStatusCode.BadRequest,
                new ArgumentError(
                    this.ModelState.Values.First(v => v.Errors.Any())
                        .Errors.Select(x => x.ErrorMessage)),
                this);
        }

        /// <summary>
        /// The not found.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <returns>
        /// The <see cref="ApiError"/>.
        /// </returns>
        protected ApiError NotFound(IError error) => new ApiError(HttpStatusCode.NotFound, error, this);

        /// <summary>
        /// The using.
        /// </summary>
        /// <typeparam name="T">
        /// Any reference type
        /// </typeparam>
        /// <returns>
        /// The <see cref="Using{T}" />.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// Type can not be resolved
        /// </exception>
        [DebuggerStepThrough]
        protected T Using<T>()
            where T : class
        {
            var resolver = StartupConfig.Config.DependencyResolver;
            var handler = resolver.GetService(typeof(T)) as T;
            if (handler == null)
            {
                throw new NullReferenceException($"Unable to resolve type with service locator; type {typeof(T).Name}");
            }

            return handler;
        }
    }
}