using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MunicipalityService.Models
{
    public class DispalyFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime d;

            string date = Convert.ToDateTime(value).ToString("MM-dd-yyyy");

            bool chValidity = DateTime.TryParseExact(
            date,
            "MM-dd-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out d);

            if (chValidity)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Date is Invalid");
        }

    }
}