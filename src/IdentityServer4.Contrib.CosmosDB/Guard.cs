using IdentityServer4.Contrib.CosmosDB.Exceptions;

namespace IdentityServer4.Contrib.CosmosDB
{
    public static class Guard
    {
        public static void ForNullOrDefault<TData>(TData source, string parameterName)
        {
            if (source == null || Equals(source, default(TData)))
                throw new ArgumentNullOrDefaultException(parameterName, $"{parameterName} cannot be null or default.");
        }
    }
}