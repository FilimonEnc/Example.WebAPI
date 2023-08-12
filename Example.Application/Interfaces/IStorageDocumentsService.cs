using Microsoft.AspNetCore.Http;

using System.IO;
using System.Threading.Tasks;

namespace Example.Application.Interfaces
{
    public interface IStorageDocumentsService
    {
        void Delete(string path);
        Task<(MemoryStream, string, string)> Download(string filename);
        Task Load(IFormFile file, string directory = "");
    }
}