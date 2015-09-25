using System;
using System.Security.Claims;

namespace LearnWordsFast.Infrastructure
{
    public static class Extensions
    {
        public static Guid GetId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.GetUserId());
        }
    }
}