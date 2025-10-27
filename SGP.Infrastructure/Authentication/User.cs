using SGP.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace SGP.Infrastructure.Authentication
{
    public class User : IdentityUser<long>
    {
        public User()
        {
            Uid = Guid.NewGuid().ToString();
        }

        public string? Name { get; set; }
        public string Uid { get; set; }
        public DateTime? Lastlogin { get; set; }
        public StatusEnum Status { get; set; }
        public UserTypeEnum Type { get; set; }
         


        public void UpdateLastlogin()
        {
            Lastlogin = DateTime.UtcNow;
        }
 

    }
}
