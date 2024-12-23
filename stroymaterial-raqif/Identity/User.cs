using Microsoft.AspNetCore.Identity;

namespace stroymaterial_raqif.Identity
{
    public class User:IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
