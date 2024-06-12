using ChatFrontend.Services.Responses;
using System.IO;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IStorageService
    {
        Task<PutFilesResponse> PutFiles(FileStream file);
    }
}
