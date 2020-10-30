using Microsoft.EntityFrameworkCore;
using MunicipalityService.DataAccess.Interfaces;
using MunicipalityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalityService.DataAccess
{
    public class TaxDataAccess : ITaxDataAccess
    {

        /// <summary>
        /// Get Tax Information by Name and Date
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <returns>Taxinformation</returns>
        public Municipality GetTaxDetails(string name, DateTime date)
        {
            using (var ctx = new MunicipalityDataContext())
            {
                var taxDetails = ctx.Municipalities.Join(ctx.Taxes,
                a => a.MunicipalityId,
                b => b.MunicipalityId,
                (a, b) => new
                {
                    Tax = b.TaxValue,
                    MunicipalityName = a.Name,
                    Dt = a.Date,
                    Id = b.FrequencyId

                }).Where(a => a.MunicipalityName == name && a.Dt == date).ToList();

                var response = taxDetails.Join(ctx.Frequencies,
                      a => a.Id,
                      b => b.FrequencyId, (a, b) => new
                      {
                          Name = a.MunicipalityName,
                          Date = a.Dt,
                          Tax = a.Tax,
                          Frequency = b.Value,
                          Order = b.Order,
                          Id = b.FrequencyId
                      }).OrderBy(x => x.Id).FirstOrDefault();


                return new Municipality()
                {
                    Name = response.Name,
                    Date = response.Date,
                    Tax = response.Tax,
                    Frequency = response.Frequency
                };
            }

        }

        /// <summary>
        /// Get all the tax details
        /// </summary>
        /// <returns>Taxinformation</returns>
        public IEnumerable<Municipality> GetALLTaxDetails()
        {
            using (var ctx = new MunicipalityDataContext())
            {
                return ctx.Taxes.Include(x => x.Municipality).Include(x => x.Frequency)
                    .Select(x => new Municipality()
                    {
                        Tax = x.TaxValue,
                        Name = x.Municipality.Name,
                        Date = x.Municipality.Date,
                        Frequency = x.Frequency.Value
                    }).ToList();
            }
        }

        /// <summary>
        /// Saving Tax Information
        /// </summary>
        /// <param name="municipality">Tax and Municipality Details</param>
        /// <returns>true</returns>

        public bool Save(Municipality municipality)
        {
            using (var ctx = new MunicipalityDataContext())
            {
                var municipalityObj = new Municipalities()
                {
                    Name = municipality.Name,
                    Date = municipality.Date,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true
                };

                ctx.Add(municipalityObj);

                ctx.SaveChanges();

                var id = ctx.Frequencies.Where(x => x.Value.ToLower().Trim() == municipality.Frequency.ToLower().Trim())
                    .Select(a => a.FrequencyId).FirstOrDefault();

                var taxObj = new Tax()
                {
                    TaxValue = municipality.Tax,
                    FrequencyId = id,
                    MunicipalityId = municipalityObj.MunicipalityId,
                };

                ctx.Add(taxObj);

                ctx.SaveChanges();

                return (taxObj.TaxId) > 0 ? true : false;
            }

        }

        /// <summary>
        /// Update Tax Information
        /// </summary>
        /// <param name="municipality"></param>
        /// <returns></returns>
        public bool Update(Municipality municipality)
        {
            using (var ctx = new MunicipalityDataContext())
            {
                var taxObj = ctx.Taxes.Include(x => x.Municipality)
                    .Include(x => x.Frequency).Where(x => x.Municipality.Name == municipality.Name
                     && x.Municipality.Date == municipality.Date
                      && x.Frequency.Value == municipality.Frequency).First();

                taxObj.TaxValue = municipality.Tax;

                ctx.SaveChanges();

                return true;
            }
        }
    }
}
