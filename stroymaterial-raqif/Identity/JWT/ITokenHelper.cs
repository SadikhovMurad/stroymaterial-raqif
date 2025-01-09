using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stroymaterial_raqif.Identity.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<string> roles);
    }
}
