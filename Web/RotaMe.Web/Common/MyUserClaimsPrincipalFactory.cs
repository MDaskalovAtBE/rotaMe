using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RotaMe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RotaMe.Web.Common
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<RotaMeUser, IdentityRole>
    {
        public MyUserClaimsPrincipalFactory(
        UserManager<RotaMeUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(RotaMeUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Avatar", user.Avatar));
            return identity;
        }
    }
}
