using System;
using System.Runtime.Serialization;

namespace IdentityServer4.Contrib.CosmosDB.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     The exception that is thrown when a null reference or the default value is passed to a method that does not accept
    ///     it as a valid argument.
    /// </summary>
    public class ArgumentNullOrDefaultException : ArgumentNullException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNullOrDefaultException"></see> class.
        /// </summary>
        public ArgumentNullOrDefaultException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNullOrDefaultException"></see> class with a specified
        ///     error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        ///     The error message that explains the reason for this exception.
        /// </param>
        /// <param name="innerException">
        ///     The exception that is thrown when a null reference or the default value is passed to a method that does not accept
        ///     it as a valid argument.
        /// </param>
        public ArgumentNullOrDefaultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNullOrDefaultException"></see> class with the name of the
        ///     parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">
        ///     The name of the parameter that caused the exception.
        /// </param>
        public ArgumentNullOrDefaultException(string paramName)
            : base(paramName)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNullOrDefaultException"></see> class with serialized
        ///     data.
        /// </summary>
        /// <param name="info">
        ///     The object that holds the serialized object data.
        /// </param>
        /// <param name="context">
        ///     An object that describes the source or destination of the serialized data.
        /// </param>
        public ArgumentNullOrDefaultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///     Initializes an instance of the <see cref="ArgumentNullOrDefaultException"></see> class with a specified error
        ///     message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">
        ///     The name of the parameter that caused the exception.
        /// </param>
        /// <param name="message">
        ///     A message that describes the error.
        /// </param>
        public ArgumentNullOrDefaultException(string paramName, string message)
            : base(paramName, message)
        {
        }
    }
}