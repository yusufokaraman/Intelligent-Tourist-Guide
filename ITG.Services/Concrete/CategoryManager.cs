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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            //.ContinueWith(t => _unitOfWork.SaveAsync());
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir.", new CategoryDto
            {

                Category = addedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir."


            });
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category);
                //.ContinueWith(t => _unitOfWork.SaveAsync());
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success


                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", new CategoryDto { 
            
            Category=null,
            ResultStatus=ResultStatus.Error,
            Message= "Böyle bir kategori bulunamadı."


            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir Kategori bulunamadı.", new CategoryListDto
            {

                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiçbir Kategori bulunamadı."

            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByCity(int cityId)
        {
            var result = await _unitOfWork.Cities.AnyAsync(c => c.Id == cityId);
            if (result)
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(a => a.CityId == cityId && !a.IsDeleted && a.IsActive, ar => ar.City);
                if (categories.Count > -1)
                {
                    return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                    {
                        Categories = categories,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategoriler bulunamadı.", null);
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Aradığınız şehir bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir Kategori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir Kategori bulunamadı.", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                //.ContinueWith(t => _unitOfWork.SaveAsync());
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategıri veritabanından başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            //.ContinueWith(t => _unitOfWork.SaveAsync());
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir.", new CategoryDto
            {

                Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message= $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir."


            });

        }
    }


}

