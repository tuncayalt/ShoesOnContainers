using System.IO;
using SystemFile = System.IO.File;

namespace ProductCatalog.Domain.Business
{
    public class PicBusiness : IPicBusiness
    {
        public byte[] GetImageById(int id, string webRootPath)
        {
            var path = Path.Combine($"{webRootPath}/Pics/", $"shoes-{id}.jpg");
            return SystemFile.ReadAllBytes(path);
        }
    }
}
