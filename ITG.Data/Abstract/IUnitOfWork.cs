using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        /// <summary>
        /// her seferinde repositoryleri ayrı ayrı çağırmamak  adına bu çalışma yapılmıştır.
        /// EntityBase veya EfEntityRepositoryBase de save metodu oluşturmadım. Veritabanına işleyebilmek adına burada implemente ediyorum.
        /// </summary>
        IArticleRepository Articles { get; } 
        ICategoryRepository Categories { get; }
        ICityRepository Cities { get; }
        ICommentRepository Comments { get; }
        IPlaceRepository Places { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }//_unitOfork.Users.AddAsync();
        Task<int> SaveAsync();

    }
}
