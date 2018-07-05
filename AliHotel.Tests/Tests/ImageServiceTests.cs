using AliHotel.Domain.Interfaces;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.IO;

namespace AliHotel.Tests.Tests
{
    [TestFixture]
    public class ImageServiceTests
    {
        private IImageService _imageService;

        [SetUp]
        public async Task Initialize()
        {
            _imageService = TestInitializer.Provider.GetService<IImageService>();
        }

        [Test]
        public async Task SaveImageFromUrl_Success()
        {
            var date = await _imageService.SaveImageFromUrl("https://cdn.101hotels.ru/uploads/image/hotel/9595/569474.jpg");

            var flag = File.Exists("D:\\Univer\\Astral\\ImageStore\\image" + date + ".png");
            Assert.That(flag, Is.EqualTo(true));
        }

        [Test]
        public async Task LoadImage_Success()
        {
            var imageName = "image05.07.2018_19-11.png";
            FileStream date = (FileStream) await _imageService.LoadImage(imageName);
            
            Assert.That(date.Name, Is.EqualTo("D:\\Univer\\Astral\\ImageStore\\" + imageName));
        }

        [Test]
        public async Task DeleteImage_Success()
        {
            var imageName = "image05.07.2018_19-11.png";
            await _imageService.DeleteImage(imageName);

            var flag = File.Exists("D:\\Univer\\Astral\\ImageStore\\" + imageName);
            Assert.That(flag, Is.EqualTo(false));
        }
    }
}
