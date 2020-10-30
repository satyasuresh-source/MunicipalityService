using MunicipalityService.Models.Utilites.Interfaces;
using System.Data;

namespace MunicipalityService.Models.Utilites
{
    public class FileProcessor : IFileProcessor
    {
        public DataTable GetData(string fileType, string filePath)
        {
            DataTable dataTable = new DataTable();
            switch (fileType)
            {
                case "Excel":
                    IFileReader excelFileReader = new ExcelFile(filePath);
                    dataTable = excelFileReader.ConvertToDataTable();
                    break;
                case "CSV":
                    IFileReader csvFileReader = new CSVFile(filePath);
                    dataTable = csvFileReader.ConvertToDataTable();
                    break;
                default:
                    break;
            }

            return dataTable;

        }
    }
}
