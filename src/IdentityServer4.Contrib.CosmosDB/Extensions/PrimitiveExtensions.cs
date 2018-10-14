using System.Linq;
using System.Net;

namespace IdentityServer4.Contrib.CosmosDB.Extensions
{
    /// <summary>
    ///     Extension methods for Primitives
    /// </summary>
    public static class PrimitiveExtensions
    {
        /// <summary>
        ///     Returns a string value or the provided default value.
        /// </summary>
        /// <param name="source">The string source.</param>
        /// <param name="defaultValue">The default value to use if source is null, empty, or whitespaces.</param>
        /// <returns></returns>
        public static string GetValueOrDefault(this string source, string defaultValue = "")
        {
            return string.IsNullOrWhiteSpace(source) ? defaultValue : source;
        }

        /// <summary>
        ///     Check if a Http Status Code is in a list of codes.
        /// </summary>
        /// <param name="source">The HTTP status code to check.</param>
        /// <param name="matchingList">The list of HTTP status codes allowed.</param>
        /// <returns></returns>
        public static bool EqualsOne(this HttpStatusCode source, params HttpStatusCode[] matchingList)
        {
            return matchingList.Contains(source);
        }
    }
}