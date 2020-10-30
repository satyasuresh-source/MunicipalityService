using MunicipalityService.DataAccess.Interfaces;
using MunicipalityService.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MunicipalityService.DataAccess
{
    public class FileAccessDataAccess : IFileAccessDataAccess
    {

        public bool DocumentUpload(DataTable dataTable)
        {
            SaveMunicipalities(dataTable);          

            SaveTaxInformation(dataTable);

            return true;

        }

        private static void SaveTaxInformation(DataTable dataTable)
        {
            var taxMapper = new TaxMapper(dataTable);

            var taxModel = taxMapper.Map();

            using (var ctx = new MunicipalityDataContext())
            {
                foreach (var dataRow in dataTable.AsEnumerable())
                {
                    var municipalityId = ctx.Municipalities.Where(x => x.Name == dataRow.Field<string>("Municipality")
                    && x.Date == Convert.ToDateTime(dataRow.Field<string>("Date")))
                         .Select(x => x.MunicipalityId).FirstOrDefault();

                    var frequencyId = ctx.Frequencies.Where(x => x.Value == dataRow.Field<string>("Frequency"))
                       .Select(x => x.FrequencyId).FirstOrDefault();

                    bool isExistingTaxID = ctx.Taxes.Any(x => x.MunicipalityId == municipalityId
                    && x.FrequencyId == frequencyId);

                    if (!isExistingTaxID)
                    {
                        var tax = new Tax()
                        {
                            TaxValue = dataRow.Field<string>("Tax"),
                            MunicipalityId = municipalityId,
                            FrequencyId = frequencyId
                        };

                        ctx.Add(tax);
                       
                    }
                }

                ctx.SaveChanges();

            }

        }

        private static void SaveFrequencies(DataTable dataTable)
        {
            var frequencyMapper = new FrequencyMapper(dataTable);

            var frequencyModel = frequencyMapper.Map();

            using (var ctx = new MunicipalityDataContext())
            {
                var frequency = frequencyModel.Select(x => new Frequency
                {
                    Value = x.Value,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }).ToList();

                ctx.BulkInsert(frequency);
            }

        }

        private static void SaveMunicipalities(DataTable dataTable)
        {
            using (var ctx = new MunicipalityDataContext())
            {                
                var municipalityMapper = new MunicipalityMapper(dataTable);

                var municipalityModel = municipalityMapper.Map();

                var existingData = ctx.Municipalities.ToList();

                foreach (var row in municipalityModel)
                {

                    bool isexistingMunicipality = existingData.Join(ctx.Taxes,
                        a => a.MunicipalityId,
                        b => b.MunicipalityId,
                        (a, b) => new
                        {
                            ID = a.MunicipalityId
                        }).Any();

                    if (!isexistingMunicipality)
                    {
                        var municipalities = new Municipalities
                        {
                            Name = row.Name,
                            IsActive = true,
                            Date = row.Date,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };

                        ctx.Add(municipalities);
                    }

                }

                ctx.SaveChanges();

            }

        }

    }
}
