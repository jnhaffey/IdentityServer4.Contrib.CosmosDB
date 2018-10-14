using static IdentityServer4.Contrib.CosmosDB.Constants;

namespace IdentityServer4.Contrib.CosmosDB
{
    /// <summary>
    ///     Static/Constant Error Messages.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        ///     Error message for when a parameter is null.
        /// </summary>
        public static string ParameterNull = $"{Placeholders.ParameterName} cannot be null.";

        /// <summary>
        ///     Error message for when a parameter is null or has a default value.
        /// </summary>
        public static string ParameterNullOrDefault = $"{Placeholders.ParameterName} cannot be null or default.";

        /// <summary>
        ///     Error message for when a value is less than another value.
        /// </summary>
        public static string ValueLessThan =
            $"{Placeholders.ParameterName} must be more than {Placeholders.MinimumValue}.";
    }
}