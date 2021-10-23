using ITG.Data.Abstract;
using ITG.Data.Concrete.EntityFramework.Contexts;
using ITG.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Burada bir interface newleyemediğimizden ötürü repositorylerin somut hallerine ihtiyacımız var. Neden somut hallerine ihtiyacımız var? Çünkü return edeceğiz.
        /// Biraz sonra new leme işlemi yapacağımdan repositoryler için readonly yapmadım.
        /// </summary>
        private readonly ITGContext _context;
        private EfArticleRepository _articleRepository;
        private EfCategoryRepository _categoryRepository;
        private EfCityRepository _cityRepository;
        private EfCommentRepository _commentRepository;
        private EfPlaceRepository _placeRepository;
        private EfRoleRepository _roleRepository;
        private EfUserRepository _userRepository;

        public UnitOfWork(ITGContext context)
        {
            _context = context;
        }

        /// <summary>
        /// İstemci Repositorylerimin interface ine ulaşıp yani IArticlesRepository'den Articles'a ulaştığında ona_articleRepository'i dönüyoruz
        /// Eğer bu değer yok veya null ise buradan yeni biri articlerepository'i new leyerek kullanıcıya gönderiyoruz.
        /// </summary>
        public IArticleRepository Articles => _articleRepository ?? new EfArticleRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);

        public ICityRepository Cities => _cityRepository ?? new EfCityRepository(_context);

        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);

        public IPlaceRepository Places => _placeRepository ?? new EfPlaceRepository(_context);

        public IRoleRepository Roles => _roleRepository ?? new EfRoleRepository(_context);

        public IUserRepository Users => _userRepository ?? new EfUserRepository(_context);

        /// <summary>
        /// Context imizin asenkron bir şekilde Dispose edilebilmesi için IUnitOfWork'te IDisposable yerine IAsyncDisposable değişikliği yapılmıştır.
        /// Bu değişiklikle ile beraber uygulamada zaman uyumsuz temizleme işlemlerini kolayca yapabileceğim.
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }


        /// <summary>
        /// SaveAsync metodumuz Contextimizin SaveChanges metoduna eşdeğer oluyor.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
