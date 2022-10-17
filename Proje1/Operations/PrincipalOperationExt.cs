using System.Security.Claims;

namespace Proje1.Operations
{
    public static class PrincipalOperationExt
    {

        public static bool CheckMailNameId(this ClaimsPrincipal principal)
        {
            if (principal.FindFirst(ClaimTypes.Email) == null || principal.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
