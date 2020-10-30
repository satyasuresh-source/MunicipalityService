using MunicipalityService.Business.Interfaces;
using MunicipalityService.DataAccess.Interfaces;
using MunicipalityService.Models;
using System;
using System.Collections.Generic;

namespace MunicipalityService.Business
{
    public class TaxManager : ITaxManager
    {
        private readonly ITaxDataAccess iTaxDataAccess;
        public TaxManager(ITaxDataAccess iTaxDataAccess)
        {
            this.iTaxDataAccess = iTaxDataAccess;
        }

        public Municipality GetTaxDetails(string name, DateTime date)
        {
            return this.iTaxDataAccess.GetTaxDetails(name, date);
        }

        public IEnumerable<Municipality> GetALLTaxDetails()
        {
            return this.iTaxDataAccess.GetALLTaxDetails();
        }

        public bool Save(Municipality municipality)
        {
            return this.iTaxDataAccess.Save(municipality);
        }

        public bool Update(Municipality municipality)
        {
            return this.iTaxDataAccess.Update(municipality);
        }
    }
}
