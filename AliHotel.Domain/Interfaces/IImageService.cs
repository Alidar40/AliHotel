using System.Threading.Tasks;

namespace AliHotel.Domain.Interfaces
{
    /// <summary>
    /// Interface for working with images
    /// </summary>
    public interface IImageService
    {
        Task<object> SaveImageFromUrl(string url);

        Task<object> LoadImage(string imageName);

        Task DeleteImage(string imageName);
    }
}
