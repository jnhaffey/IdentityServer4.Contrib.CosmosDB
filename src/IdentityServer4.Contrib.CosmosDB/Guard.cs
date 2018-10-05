using System;
using IdentityServer4.Contrib.CosmosDB.Exceptions;
using static IdentityServer4.Contrib.CosmosDB.Constants;

namespace IdentityServer4.Contrib.CosmosDB
{
    /// <summary>
    ///     Guard Class
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Test for a null value.
        /// </summary>
        /// <typeparam name="TData">Type of source.</typeparam>
        /// <param name="source">Source to test.</param>
        /// <param name="parameterName">Name of parameter.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if null is found.</exception>
        public static void ForNull<TData>(TData source, string parameterName)
        {
            if (source == null)
                throw new ArgumentNullException(parameterName, ErrorMessages.ParameterNull
                    .Replace(Placeholders.ParameterName, parameterName));
        }

        /// <summary>
        ///     Test for null or default value.
        /// </summary>
        /// <typeparam name="TData">Type of source.</typeparam>
        /// <param name="source">Source to test.</param>
        /// <param name="parameterName">Name of parameter.</param>
        /// <exception cref="ArgumentNullOrDefaultException">Exception thrown if null or default value is found.</exception>
        public static void ForNullOrDefault<TData>(TData source, string parameterName)
        {
            if (source == null || Equals(source, default(TData)))
                throw new ArgumentNullOrDefaultException(parameterName, ErrorMessages.ParameterNullOrDefault
                    .Replace(Placeholders.ParameterName, parameterName));
        }

        /// <summary>
        ///     Test for value less than minimum value.
        /// </summary>
        /// <param name="sourceValue">Source to test.</param>
        /// <param name="minimumValue">Minimum value.</param>
        /// <param name="parameterName">Name of parameter.</param>
        /// <exception cref="ArgumentException">Exception thrown if value is less than minimum value.</exception>
        public static void ForValueLessThan(int sourceValue, int minimumValue, string parameterName)
        {
            if (sourceValue < minimumValue)
                throw new ArgumentException(
                    ErrorMessages.ValueLessThan.Replace(Placeholders.ParameterName, parameterName)
                        .Replace(Placeholders.MinimumValue, minimumValue.ToString()), parameterName);
        }
    }
}