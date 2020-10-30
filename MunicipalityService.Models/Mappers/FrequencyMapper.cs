using System.Data;
using System.Collections.Generic;

namespace MunicipalityService.Models.Mappers
{
    public class FrequencyMapper : BaseMappper<Frequency>
    {
        private readonly DataTable dataTable;
        public FrequencyMapper(DataTable dataTable)
        {
            this.dataTable = dataTable;
        }
        public override IEnumerable<Frequency> Map()
        {
            return dataTable.AsEnumerable()
                .Select(x => new Frequency
                {
                    Value = x.Field<string>("Frequency")                    
                });
        }
    }
}
