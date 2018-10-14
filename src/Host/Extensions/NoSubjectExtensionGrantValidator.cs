using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Host.Extensions
{
    public class NoSubjectExtensionGrantValidator : IExtensionGrantValidator
    {
        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var credential = context.Request.Raw.Get("custom_credential");

            context.Result = credential != null
                ? new GrantValidationResult()
                : new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");

            return Task.CompletedTask;
        }

        public string GrantType => "custom.nosubject";
    }
}