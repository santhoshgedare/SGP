using Microsoft.AspNetCore.Identity;

namespace SGP.Infrastructure.Authentication
{
    public class Role : IdentityRole<long>
    {
        public Role()
        {
            Uid = Guid.NewGuid().ToString();
        }
        public void Update(string name, string normalizedName, string landingPage)
        {
            Name = name;
            NormalizedName = normalizedName;
            LandingPage = landingPage;
        }
        public string? LandingPage { get; set; }
        public string Uid { get; set; }
    }
}
