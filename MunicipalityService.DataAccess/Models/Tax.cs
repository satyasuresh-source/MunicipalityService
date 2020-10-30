using System.ComponentModel.DataAnnotations;

namespace MunicipalityService.DataAccess
{
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }

        [Required]
        public string TaxValue { get; set; }
        public Municipalities Municipality { get; set; }
        public int MunicipalityId { get; set; }
        public Frequency Frequency { get; set; }
        public int FrequencyId { get; set; }
    }
}
