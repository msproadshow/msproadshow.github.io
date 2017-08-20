namespace RoadShowHardCode.BusinessLayer
{
    using System.Collections.Generic;

    /// <summary>
    /// The handler result.
    /// </summary>
    /// <typeparam name="T">
    /// Any type
    /// </typeparam>
    public class HandlerResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerResult{T}"/> class.
        /// </summary>
        public HandlerResult()
        {
            this.Errors = new List<IError>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerResult{T}"/> class.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        public HandlerResult(IError error)
            : this()
        {
            if (error?.Errors != null)
            {
                this.Errors.Add(error);
            }
            else
            {
                this.Succeeded = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerResult{T}"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public HandlerResult(IList<IError> errors)
            : this()
        {
            if (errors != null && errors.Count > 0)
            {
                this.Errors = errors;
            }
            else
            {
                this.Succeeded = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerResult{T}"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public HandlerResult(IList<IError> errors, T data)
            : this(errors)
        {
            this.Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerResult{T}"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public HandlerResult(T data)
            : this()
        {
            this.Data = data;
            this.Succeeded = true;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        public IList<IError> Errors { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// Gets a value indicating whether succeeded.
        /// </summary>
        public bool Succeeded { get; }
    }
}