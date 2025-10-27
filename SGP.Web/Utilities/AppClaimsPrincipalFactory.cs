using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SGP.Infrastructure.Authentication;
using System.Security.Claims;

namespace SGP.Web.Utilities
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public AppClaimsPrincipalFactory(UserManager<User> userManager,
                                         RoleManager<Role> roleManager,
                                         IOptions<IdentityOptions> optionsAccessor
                                        ) : base(userManager, roleManager, optionsAccessor) { }

        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            new Claim("Type",user.Type.ToString()),
            new Claim("DisplayName", user.Name ),
        });

            return principal;
        }

    }
}
