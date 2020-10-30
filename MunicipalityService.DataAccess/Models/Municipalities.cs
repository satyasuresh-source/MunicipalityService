using System;
using System.ComponentModel.DataAnnotations;

namespace MunicipalityService.DataAccess
{
    public class Municipalities
    {

        [Key]
        public int MunicipalityId { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        //public int TaxId { get; set; }
        //public Tax Tax { get; set; }

    }
}
