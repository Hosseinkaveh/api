using System.Security.Claims;

namespace Api.Entities
{
    public static class ClaimsPrincipleExtension
    {
        public static string GetUsername(this ClaimsPrincipal User)
        {
           return User.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int GetUserId(this ClaimsPrincipal User)
        {
           return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}