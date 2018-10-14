using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Host.Extensions
{
    public class ExtensionGrantValidator : IExtensionGrantValidator
    {
        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var credential = context.Request.Raw.Get("custom_credential");

            context.Result = credential != null
                ? new GrantValidationResult("818727", "custom")
                : new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");

            return Task.CompletedTask;
        }

        public string GrantType => "custom";
    }
}