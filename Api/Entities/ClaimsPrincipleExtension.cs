using System.Security.Claims;

namespace Api.Entities
{
    public static class ClaimsPrincipleExtension
    {
        public static string GetUsername(this ClaimsPrincipal User)
        {
           return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}