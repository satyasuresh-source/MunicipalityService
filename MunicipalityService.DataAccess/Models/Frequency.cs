using System;
using System.ComponentModel.DataAnnotations;

namespace MunicipalityService.DataAccess
{
    public class Frequency
    {
        [Key]
        public int FrequencyId { get; set; }

        [Required]
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }     
        public int Order { get; set; }
        
    }
}
