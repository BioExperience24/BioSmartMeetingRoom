
using Microsoft.AspNetCore.Authorization;

namespace _5.Helpers.Consumer.Policy
{
    public static class AuthorizationWebviewPolicies
    {
        public const string OnlyWebview = "OnlyWebview";
        public const string OnlyNonWebview = "OnlyNonWebview";

        public static void AddCustomPolicies(AuthorizationOptions options)
        {
            options.AddPolicy(OnlyWebview, policy =>
                policy.RequireClaim("IsWebview", "true"));

            options.AddPolicy(OnlyNonWebview, policy =>
                policy.RequireClaim("IsWebview", "false"));
        }
    }
}