using System.Security.Claims;

namespace Api.Extensions
{
    public static class UserExtensions
    {
        private const string AccountIdClaimType = "accountId";
        private const string TenantIdClaimType = "tenantId";

        public static string GetAccountId(this ClaimsPrincipal context)
        {
            return context.FindFirstValue(AccountIdClaimType);
        }
        public static string GetTenantId(this ClaimsPrincipal context)
        {
            return context.FindFirstValue(TenantIdClaimType);
        }
    }

}
