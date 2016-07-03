using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace GreatPlacesInPh.Extensions
{
    public static class UserExtensions
    {
        public static string GetUserId(this System.Security.Principal.IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("UserId");

            if (claim != null && claim.Value != " ")
                return claim.Value;

            return String.Empty;
        }
    }
}