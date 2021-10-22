using ITG.Data.Abstract;
using ITG.Entities.Concrete;
using ITG.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Data.Concrete.EntityFramework.Repositories
{
    public class EfCityRepository : EfEntityRepositoryBase<City>, ICityRepository
    {
        public EfCityRepository(DbContext context) : base(context)
        {
        }
    }
}
