using ITG.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

/// <summary>
/// Bir başka projede kullanılabilme durumuna karşın IEntityRepository DataAccessLayer'da değil de Share klasörünün altında açılmıştır.
/// Burada tüm projeler altında kullanılabilecek DAL classları metotları oluşturulmuştur.
/// </summary>
namespace ITG.Shared.Data.Abstract
{

    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        /// <summary>
        /// Projede vereceğim lambda expressionlar Örn:var article=repository.GetAsync(m=>m.Id==15); yani Idsi 15 olan makaleyi getir diyerek filtre kullanıyorum. Bu da predicate'dir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T,bool>>predicate,
            params Expression<Func<T,object>>[] includeProperties);
        /// <summary>
        /// Liste halinde cekmek icin GetAllAsync ile yeni bir Task oluşturulmuştur.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate=null,
            params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Gelen T tipindeki entity nesnemizi eklemek için oluşturulmuştur.
        /// Ajax ev Jquery işlemleri için güncellenmiştir.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Update operasyonları içiçn oluşturulmuştur.
        /// Ajax ev Jquery işlemleri için güncellenmiştir.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);
        
        /// <summary>
        /// Delete işlemleri için oluşturulmuştur.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
        
        /// <summary>
        /// Any metodu daha önce ilgili işlem var mı diye kontrol etmeka adına yazılmıştır. Örnek vermek gerekirse daha önce böyle bir kullanıcı  maili kullanılmış mı?
        /// </summary>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Sayım işlemlerinde kullanmak adına oluşturulmuştur.
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
