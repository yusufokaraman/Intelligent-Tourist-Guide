using ITG.Data.Abstract;
using ITG.Data.Concrete;
using ITG.Data.Concrete.EntityFramework.Contexts;
using ITG.Entities.Concrete;
using ITG.Services.Abstract;
using ITG.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection servicecollection)
        {
            /// DbContext'te bir scope tur. Bir istekte bulunulduğunda ve işlemlere başlanıldığında bu işlemlerin bütünü bir scope
            /// içerisinde alınır ve orada yürütülür.Tüm işlemler bittikten sonra da örnek olarak buradaki siteyle bağlantıyı kestiğimizi düşünelim.
            /// Bu durumda buradaki scope da kendisini kapatır.DbContext'imiz de bu şekilde çalıştığı için UnitOfWork yapımızı da diğer servislerimizi de bu şekilde kayıt edeceğiz.
            servicecollection.AddDbContext<ITGContext>();
            servicecollection.AddIdentity<User, Role>(options => {

                //User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                //User Username and Email Options

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;




            }).AddEntityFrameworkStores<ITGContext>();
            servicecollection.AddScoped<IUnitOfWork, UnitOfWork>();
            servicecollection.AddScoped<ICategoryService, CategoryManager>();
            servicecollection.AddScoped<IArticleService, ArticleManager>();
            servicecollection.AddScoped<ICityService, CityManager>();
            servicecollection.AddScoped<IPlaceService, PlaceManager>();
            return servicecollection;
        }
    }
}
