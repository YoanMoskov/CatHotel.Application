namespace CatHotel.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtension
    {
        public static string GetId(this ClaimsPrincipal principal)
            => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}

