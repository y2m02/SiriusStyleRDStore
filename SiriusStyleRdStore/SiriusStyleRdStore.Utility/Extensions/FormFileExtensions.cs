using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SiriusStyleRdStore.Utility.Extensions
{
    public static class FormFileExtensions
    {
        public static string UploadFile(this IFormFile image, IWebHostEnvironment webHostEnvironment)
        {
            if (image.IsEmpty()) return null;

            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/products");
            var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            image.CopyTo(fileStream);

            return uniqueFileName;
        }
    }
}