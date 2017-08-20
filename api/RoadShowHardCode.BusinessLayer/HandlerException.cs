namespace RoadShowHardCode.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// The handler exception.
    /// </summary>
    [Serializable]
    public class HandlerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerException"/> class.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public HandlerException(IEnumerable<IError> errors)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public HandlerException(string message, IEnumerable<IError> errors)
            : base(message)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public HandlerException(string message, Exception innerException, IEnumerable<IError> errors)
            : base(message, innerException)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.
        /// </param>
        /// <param name="errors">
        /// The errors.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected HandlerException(SerializationInfo info, StreamingContext context, IEnumerable<IError> errors)
            : base(info, context)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        public IEnumerable<IError> Errors { get; }

        /// <summary>Creates and returns a string representation of the current exception.</summary>
        /// <returns>A string representation of the current exception.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        /// </PermissionSet>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(
                this.Errors.Any() ? "Handler failed with this errors:" : "Handler failed with some errors");

            foreach (var error in this.Errors)
            {
                builder.AppendLine($"\t Code: {error.ErrorCode}{Environment.NewLine}\t Errors:");
                builder.Append("\t\t");

                using (var enumerator = error.Errors.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        for (builder.Append(enumerator.Current); enumerator.MoveNext();)
                        {
                            builder.Append(", ");
                            builder.Append(enumerator.Current);
                        }
                    }
                }

                builder.AppendLine();
            }

            builder.AppendLine(base.ToString());

            return builder.ToString();
        }
    }
}