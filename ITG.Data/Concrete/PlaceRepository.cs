using ITG.Data.Abstract;
using ITG.Entities.Concrete;
using ITG.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ITG.Data.Concrete
{
    public class PlaceRepository : EfEntityRepositoryBase<Place>, IPlaceRepository
    {
        public PlaceRepository(DbContext context) : base(context)
        {
        }
    }
}
