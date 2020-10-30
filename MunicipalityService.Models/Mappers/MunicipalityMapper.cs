using System;
using System.Collections.Generic;
using System.Data;


namespace MunicipalityService.Models.Mappers
{
    public class MunicipalityMapper : BaseMappper<Municipality>
    {
        private readonly DataTable dataTable;
        public MunicipalityMapper(DataTable dataTable)
        {
            this.dataTable = dataTable;
        }
        public override IEnumerable<Municipality> Map()
        {
           return dataTable.AsEnumerable()
                .Select(x => new Municipality
                {
                    Name = x.Field<string>("Municipality"),
                    Date = Convert.ToDateTime(x.Field<string>("Date"))
                });
            
        }
    }
}
