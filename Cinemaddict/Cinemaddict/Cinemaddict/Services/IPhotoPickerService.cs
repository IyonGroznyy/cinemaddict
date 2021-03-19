using System.IO;
using System.Threading.Tasks;

namespace Cinemaddict.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
