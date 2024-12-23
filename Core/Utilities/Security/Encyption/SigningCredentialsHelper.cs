using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encyption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey secutiryKey)
        {
            return new SigningCredentials(secutiryKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
