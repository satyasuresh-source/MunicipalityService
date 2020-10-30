using System.ComponentModel.DataAnnotations;

namespace MunicipalityService.Models
{
    public class FileAccessRequest
    {
        [Required(ErrorMessage = "FileType is Required")]
        public string FileType { get; set; }

        [Required(ErrorMessage = "FilePath is Required")]
        public string FilePath { get; set; }      

    }
}
