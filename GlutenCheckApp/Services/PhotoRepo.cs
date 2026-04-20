using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlutenCheckApp.Interfaces;

namespace GlutenCheckApp.Services
{
    public class PhotoRepo : IPhotoRepo
    {
        public Task<string> GetPhoto(string photoUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SavePhoto(FileResult photo)
        {
            if (photo == null) throw new ArgumentNullException("No photo data, or missing!");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";

            var targetPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            using var sourceStream = await photo.OpenReadAsync();
            using var targetStream = File.Create(targetPath);

            await sourceStream.CopyToAsync(targetStream);

            return targetPath;
        }
    }
}
