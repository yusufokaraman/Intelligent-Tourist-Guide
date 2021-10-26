using AutoMapper;
using ITG.Data.Abstract;
using ITG.Entities.Concrete;
using ITG.Entities.Dtos;
using ITG.Services.Abstract;
using ITG.Shared.Utilities.Results.Abstract;
using ITG.Shared.Utilities.Results.ComplexTypes;
using ITG.Shared.Utilities.Results.Concrete;
using System;
using System.Threading.Tasks;

namespace ITG.Services.Concrete
{
    public class CityManager : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CityManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async  Task<IResult> Add(CityAddDto cityAddDto, string createdByName)
        {
            var city = _mapper.Map<City>(cityAddDto);
            city.CreatedByName = createdByName;
            city.ModifiedByName = createdByName;
            await _unitOfWork.Cities.AddAsync(city)
           .ContinueWith(t => _unitOfWork.SaveAsync());
            //await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{cityAddDto.Name} adlı şehir başarıyla eklenmiştir.");
        }

        public async  Task<IResult> Delete(int cityId, string modifiedByName)
        {
            var city = await _unitOfWork.Cities.GetAsync(c => c.Id == cityId);
            if (city != null)
            {
                city.IsDeleted = true;
                city.ModifiedByName = modifiedByName;
                city.ModifiedDate = DateTime.Now;
                await _unitOfWork.Cities.UpdateAsync(city).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{city.Name} adlı şehir başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }

        public async Task<IDataResult<CityDto>> Get(int cityId)
        {
            var city = await _unitOfWork.Cities.GetAsync(c => c.Id == cityId, c => c.Articles, c=>c.Places, c=>c.Categories);
            if (city != null)
            {
                return new DataResult<CityDto>(ResultStatus.Success, new CityDto
                {
                    City = city,
                    ResultStatus = ResultStatus.Success


                });
            }
            return new DataResult<CityDto>(ResultStatus.Error, "Böyle bir şehir bulunamadı.", null);
        }

        public async Task<IDataResult<CityListDto>> GetAll()
        {
            var cities = await _unitOfWork.Cities.GetAllAsync(null, c => c.Articles, c => c.Places, c => c.Categories);
            if (cities.Count > -1)
            {
                return new DataResult<CityListDto>(ResultStatus.Success, new CityListDto
                {
                    Cities = cities,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<CityListDto>(ResultStatus.Error, "Hiçbir şehir bulunamadı.", null);
        }


        public async Task<IDataResult<CityListDto>> GetAllByNonDeleted()
        {
            var cities = await _unitOfWork.Cities.GetAllAsync(c => !c.IsDeleted, c => c.Articles, c => c.Categories, c => c.Places);
            if (cities.Count > -1)
            {
                return new DataResult<CityListDto>(ResultStatus.Success, new CityListDto
                {
                    Cities = cities,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CityListDto>(ResultStatus.Error, "Hiçbir Şehir bulunamadı.", null);
        }

        public async Task<IDataResult<CityListDto>> GetAllByNonDeletedAndActive()
        {
            var cities  = await _unitOfWork.Cities.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles, c => c.Categories, c => c.Places);
            if (cities.Count > -1)
            {
                return new DataResult<CityListDto>(ResultStatus.Success, new CityListDto
                {
                    Cities = cities,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CityListDto>(ResultStatus.Error, "Hiçbir Şehir bulunamadı.", null);
        }

        public async Task<IResult> HardDelete(int cityId)
        {
            var city = await _unitOfWork.Cities.GetAsync(c => c.Id == cityId);
            if (city != null)
            {
                await _unitOfWork.Cities.DeleteAsync(city).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{city.Name} adlı şehir veritabanından başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir şehir bulunamadı.");
        }

        public async Task<IResult> Update(CityUpdateDto cityUpdateDto, string modifiedByName)
        {
            var city = _mapper.Map<City>(cityUpdateDto);
            city.ModifiedByName = modifiedByName;
            await _unitOfWork.Cities.UpdateAsync(city).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{cityUpdateDto.Name} adlı şehir başarıyla güncellenmiştir.");
        }
    }
}
