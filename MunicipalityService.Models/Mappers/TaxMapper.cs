using System.Collections.Generic;
using System.Data;

namespace MunicipalityService.Models.Mappers
{
    public class TaxMapper : BaseMappper<Tax>
    {
        private readonly DataTable dataTable;
        public TaxMapper(DataTable dataTable)
        {
            this.dataTable = dataTable;
        }
        public override IEnumerable<Tax> Map()
        {
            return dataTable.AsEnumerable()
                .Select(x => new Tax
                {
                    TaxValue = x.Field<string>("Tax")
                });
        }
    }
}
