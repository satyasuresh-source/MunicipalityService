using System.Data;
using MunicipalityService.Business.Interfaces;
using MunicipalityService.DataAccess.Interfaces;
using MunicipalityService.Models;
using MunicipalityService.Models.Utilites.Interfaces;
using System.ComponentModel;

namespace MunicipalityService.Business
{
    public class FileAcessManager : IFileAccessManager
    {
        private readonly IFileAccessDataAccess iFileAccessDataAccess;

        private readonly IFileProcessor iFileProcessor;
        public FileAcessManager(IFileAccessDataAccess iFileAccessDataAccess,
            IFileProcessor iFileProcessor)
        {
            this.iFileAccessDataAccess = iFileAccessDataAccess;
            this.iFileProcessor = iFileProcessor;
        }
              
        public bool DocumentUpload(FileAccessRequest accessRequest)
        {
            var dataTable = GetData(accessRequest.FileType, accessRequest.FilePath);

            return iFileAccessDataAccess.DocumentUpload(dataTable);           
        }

        private DataTable GetData(string fileType, string filePath)
        {
            return iFileProcessor.GetData(fileType, filePath);
        }

    }
}
