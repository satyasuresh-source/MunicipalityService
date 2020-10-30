using MunicipalityService.Models;

namespace MunicipalityService.Business.Interfaces
{
    public interface IFileAccessManager
    {
        bool DocumentUpload(FileAccessRequest request);

    }
}
