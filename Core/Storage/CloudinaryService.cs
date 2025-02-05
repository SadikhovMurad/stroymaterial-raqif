using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Storage
{
    public class CloudinaryService : ICloudinaryService
    {
        Cloudinary cloudinary = new Cloudinary(new Account("dkeckqqcp", "858845549972217", "WxuIdzk_BkZWH1GMu-acmBR4cd8"));

        public Uri Upload(IFormFile image)
        {
            var result = cloudinary.Upload(new ImageUploadParams
            {
                File = new FileDescription(image.FileName,
                        image.OpenReadStream())
            });
            return result.Url;
        }
    }
}
