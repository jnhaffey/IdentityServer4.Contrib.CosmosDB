using System;
using System.Runtime.Serialization;

namespace IdentityServer4.Contrib.CosmosDB.Exceptions
{
    public class ArgumentNullOrDefaultException : ArgumentNullException
    {
        public ArgumentNullOrDefaultException()
        {
        }

        public ArgumentNullOrDefaultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ArgumentNullOrDefaultException(string paramName)
            : base(paramName)
        {
        }

        public ArgumentNullOrDefaultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ArgumentNullOrDefaultException(string paramName, string message)
            : base(paramName, message)
        {
        }
    }
}