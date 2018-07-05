using AliHotel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AliHotel.Domain.Services
{
    public class ImageService : IImageService
    {
        public async Task<object> SaveImageFromUrl(string url)
        {
            var date = DateTime.UtcNow.ToShortDateString() + "_" + DateTime.UtcNow.ToShortTimeString().Replace(':', '-');

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url), "D:\\Univer\\Astral\\ImageStore\\image" + date + ".png");
            }
            return date;
        }
        
        public async Task<object> LoadImage(string imageName)
        {
            var image = System.IO.File.OpenRead("D:\\Univer\\Astral\\ImageStore\\" + imageName);
            return await Task.FromResult(image);
        }

        public async Task DeleteImage(string imageName)
        {
            System.IO.File.Delete("D:\\Univer\\Astral\\ImageStore\\" + imageName);
        }
    }
}
