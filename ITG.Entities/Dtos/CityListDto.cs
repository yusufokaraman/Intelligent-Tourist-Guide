using ITG.Entities.Concrete;
using ITG.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Entities.Dtos
{
    public class CityListDto:DtoGetBase
    {
        public IList<City> Cities { get; set; }
    }
}
