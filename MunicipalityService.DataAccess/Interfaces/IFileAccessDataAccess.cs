using MunicipalityService.Models;
using System.Data;

namespace MunicipalityService.DataAccess.Interfaces
{
    public interface IFileAccessDataAccess
    {
        bool DocumentUpload(DataTable dataTable);
    }
}
