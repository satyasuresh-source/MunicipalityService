using System;
using System.ComponentModel.DataAnnotations;

namespace MunicipalityService.Models
{
    public class Municipality
    {
        [Required(ErrorMessage = "Municipality Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [DispalyFormat]
        public DateTime Date { get; set; }
        public string Tax { get; set; }
        public string Frequency { get; set; }

    }
}
