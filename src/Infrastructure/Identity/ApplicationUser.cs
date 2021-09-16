using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string ConciergeID { get; set; } = null;

        public string FullName { get; set; }

        public string Gender { get; set; }
    }
}
