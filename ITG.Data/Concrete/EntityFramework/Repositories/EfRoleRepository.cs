using ITG.Data.Abstract;
using ITG.Entities.Concrete;
using ITG.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ITG.Data.Concrete.EntityFramework.Repositories
{
    public class EfRoleRepository : EfEntityRepositoryBase<Role>, IRoleRepository
    {
        public EfRoleRepository(DbContext context) : base(context)
        {
        }
    }
}
