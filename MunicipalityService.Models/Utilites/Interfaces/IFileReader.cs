using System.Data;

namespace MunicipalityService.Models.Utilites.Interfaces
{
    public interface IFileReader
    {
        DataTable ConvertToDataTable();

    }
}
