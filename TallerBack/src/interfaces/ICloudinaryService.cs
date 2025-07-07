using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.interfaces
{
    public interface ICloudinaryService
    {
        Task<(string Url, string PublicId)> UploadImageAsync(IFormFile file);
        Task<List<string>> UploadImagesAsync(List<IFormFile> files);
        Task<bool> DeleteImageAsync(string publicId);
    }
}