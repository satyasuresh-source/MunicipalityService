using System.Collections.Generic;
namespace MunicipalityService.Models.Mappers
{
    public abstract class BaseMappper<T>
    {
        public abstract IEnumerable<T> Map();

    }
}
