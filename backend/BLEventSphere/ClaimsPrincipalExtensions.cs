using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLEventSphere
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst("userId");

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                throw new UnauthorizedAccessException("UserId claim not found");
            }

            return long.Parse(claim.Value);
        }

        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.FindFirst("role")!.Value;
        }
    }
}
