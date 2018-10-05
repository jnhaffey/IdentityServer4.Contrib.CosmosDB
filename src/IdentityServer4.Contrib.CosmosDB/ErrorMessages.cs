using static IdentityServer4.Contrib.CosmosDB.Constants;

namespace IdentityServer4.Contrib.CosmosDB
{
    public static class ErrorMessages
    {
        public static string ParameterNull = $"{Placeholders.ParameterName} cannot be null.";
        public static string ParameterNullOrDefault = $"{Placeholders.ParameterName} cannot be null or default.";

        public static string ValueLessThan =
            $"{Placeholders.ParameterName} must be more than {Placeholders.MinimumValue}.";
    }
}