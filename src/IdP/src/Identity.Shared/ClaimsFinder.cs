using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityModel;

namespace Identity.Shared
{
    public static class ClaimsFinder
    {
        public static Claim ResolveEmailClaim(this ICollection<Claim> claims)
        {
            var emailClaim = claims.FirstOrDefault(JwtClaimTypes.Email,
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");

            return emailClaim;
        }

        public static Claim ResolveNameClaim(string userId, ICollection<Claim> claims)
        {
            var nameClaim = claims.FirstOrDefault(JwtClaimTypes.Name,
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");

            if (nameClaim == null)
            {
                claims.Add(new Claim(JwtClaimTypes.Name, userId));
            }
            return nameClaim;
        }

        public static Claim ResolveSubjectClaim(this ICollection<Claim> claims)
        {
            return claims.SingleOrDefault(x =>
                x.Type.Equals(JwtClaimTypes.Subject, StringComparison.OrdinalIgnoreCase));
        }

        public static Claim FirstOrDefault(this ICollection<Claim> claims, params string[] typeOrder)
        {
            return typeOrder
                .Select(type => claims.FirstOrDefault(x => x.Type.Equals(type, StringComparison.OrdinalIgnoreCase)))
                .FirstOrDefault(claim => claim != null);
        }
    }
}