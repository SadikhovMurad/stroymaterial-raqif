using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Storage
{
    public interface ICloudinaryService
    {
        Uri Upload(IFormFile image);
    }
}
