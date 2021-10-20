using ITG.Data.Abstract;
using ITG.Entities.Concrete;
using ITG.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Data.Concrete
{
    public class CityRepository : EfEntityRepositoryBase<City>, ICityRepository
    {
        public CityRepository(DbContext context) : base(context)
        {
        }
    }
}
