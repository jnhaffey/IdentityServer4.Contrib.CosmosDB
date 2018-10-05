using System;
using IdentityServer4.Contrib.CosmosDB.Exceptions;

namespace IdentityServer4.Contrib.CosmosDB
{
    public static class Guard
    {
        public static void ForNull<TData>(TData source, string parameterName)
        {
            if (source == null)
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null.");
        }

        public static void ForNullOrDefault<TData>(TData source, string parameterName)
        {
            if (source == null || Equals(source, default(TData)))
                throw new ArgumentNullOrDefaultException(parameterName, $"{parameterName} cannot be null or default.");
        }

        public static void ForValueLessThan(int sourceValue, int minimumValue, string parameterName)
        {
            if (sourceValue < minimumValue)
                throw new ArgumentException($"{parameterName} must be more than {minimumValue}.", parameterName);
        }
    }
}