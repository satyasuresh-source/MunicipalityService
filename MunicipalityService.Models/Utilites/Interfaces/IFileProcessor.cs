using System.Data;

namespace MunicipalityService.Models.Utilites.Interfaces
{
    public interface IFileProcessor
    {
        DataTable GetData(string fileType, string filePath);
    }
}
