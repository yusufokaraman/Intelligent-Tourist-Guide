using AutoMapper;
using ITG.Data.Abstract;
using ITG.Entities.Concrete;
using ITG.Entities.Dtos;
using ITG.Services.Abstract;
using ITG.Shared.Utilities.Results.Abstract;
using ITG.Shared.Utilities.Results.ComplexTypes;
using ITG.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Services.Concrete
{
    /// <summary>
    /// Add,Update,Delete ve Hard Delete bölümlerinde değişiklik yapılmıştır.
    /// </summary>
    

    /// Update,Delete,Add ve Hard Delete kısımlarında DbContext'in Thread-Safe olmamasından kaynaklı ".ContinueWith(t => _unitOfWork.SaveAsync());" "await _unitOfWork.SaveAsync();"
    /// arasında değişikliğe gidilmiştir. Thread-Safe farklı thread'ler üzerinden yani farklı iş parçacıkları üzerinden DbContext'i çağırmak ve kullanmaktır.
    /// Daha açık olmak gerekirse AddAsync metodu çağırıldığında SaveAsync metodunu bir task olarak veriyoruz.Await keywordü sayesinde AddAsync'in bitmesini bekliyoruz.
    /// Metot bittiği gibi arkaplanda SaveAsync metodunu çalıştırıyoruz.Dolayısıyla bu işlem arkplanda çalıştığı için farklı bir iş parçacığında çalışıyor.Diğer thread bir alt satırdan devam edip
    /// controller içine geri döndüğünde arkplanda da bir tane SaveAsync metodu çalışıyor. Bu bize büyük oranda hız kazandırıyor.
    /// Sorun işe şurda oluşmakta; controller a geri döndüğümüzde -özellikle AJAX işlemlerinde başımıza gelmek- bizler hala istek yapmaya devam edip farklı bir istekte bulunduğumuzda Örnk:GetAll();
    /// Arkaplanda DbContext SaveAsync metodu ile kayıt edilirken bizler tekrar DbContext'i ikinci bir iş parçacığından kullanmaya çalıştığımızda bizlere bir hata fırlatmakta. Bize thread üzerinde
    /// halihazırda bri DbContex'in çalıştığını, ikinci bir thread'in bunu beklemesini gerektiğini söylemekte.Kısaca hızlı işliyor olmamızdan bir hata ile karşılaşmaktayız. 
    /// Bu sorunu yine await ile çözmekteyiz. "await _unitOfWork.SaveAsync();" satırındaki await işlemi ilgili kod parçaçığı bittikten sonra return işlemini  yapıp controller a geri dönmekte. 
    /// Bu sayede her bir thread kendi DbContext'i ile çalıştığı için bizler bu Scope içerisinde hiçbir sorun yaşamadan işlemlerimiz gerçekleştiriyoruz.
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;
            await _unitOfWork.Articles.AddAsync(article);
            //.ContinueWith(t=>_unitOfWork.SaveAsync());
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{articleAddDto.Title} başlıklı makale başarıyla eklenmiştir.");
        }

        public async  Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if(result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await _unitOfWork.Articles.UpdateAsync(article);
                //.ContinueWith(t => _unitOfWork.SaveAsync());
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadı.");
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId,a=>a.User,a=>a.Category,a=>a.City,a=>a.Place);
            if(article!=null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success,new ArticleDto 
                { 
                Article=article,
                ResultStatus=ResultStatus.Success
                
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, "Aradığınız makale bulunamadı.", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, a=>a.User,a=>a.Category,a=>a.City,a=>a.Place);
            if(articles.Count>-1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı.", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if(result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı.", null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Aradığınız kategori bulunamadı.", null);

        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCity(int cityId)
        {
            var result = await _unitOfWork.Cities.AnyAsync(c => c.Id == cityId);
            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CityId == cityId && !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.City);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı.", null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Aradığınız şehir bulunamadı.", null);
        }

        public async  Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, ar => ar.User, ar => ar.Category, ar => ar.City, ar => ar.Place);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı.", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category, ar => ar.City, ar => ar.Place);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı.", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByPlace(int placeId)
        {
            var result = await _unitOfWork.Places.AnyAsync(c => c.Id == placeId);
            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.PlaceId == placeId && !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Place);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı.", null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Aradığınız mekan bulunamadı.", null);
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                await _unitOfWork.Articles.DeleteAsync(article);
                //.ContinueWith(t => _unitOfWork.SaveAsync());
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadı.");
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;
            await _unitOfWork.Articles.UpdateAsync(article);
            //.ContinueWith(t=>_unitOfWork.SaveAsync());
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{articleUpdateDto.Title} başlıklı makale başarıyla güncellenmiştir.");
        }
    }
}
