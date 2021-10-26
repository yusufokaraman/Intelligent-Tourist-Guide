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
    public class PlaceManager : IPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlaceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult> Add(PlaceAddDto placeAddDto, string createdByName)
        {
            var place = _mapper.Map<Place>(placeAddDto);
            place.CreatedByName = createdByName;
            place.ModifiedByName = createdByName;
            await _unitOfWork.Places.AddAsync(place).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{placeAddDto.Name} başlıklı mekan başarıyla eklenmiştir.");
        }

        public async Task<IResult> Delete(int placeId, string modifiedByName)
        {
            var result = await _unitOfWork.Places.AnyAsync(a => a.Id == placeId);
            if (result)
            {
                var place = await _unitOfWork.Places.GetAsync(a => a.Id == placeId);
                place.IsDeleted = true;
                place.ModifiedByName = modifiedByName;
                place.ModifiedDate = DateTime.Now;
                await _unitOfWork.Places.UpdateAsync(place).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{place.Name} başlıklı mekan başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir mekan bulunamadı.");
        }

        public async Task<IDataResult<PlaceDto>> Get(int placeId)
        {
            var place = await _unitOfWork.Places.GetAsync(a => a.Id == placeId, a => a.Category, a => a.City);
            if (place != null)
            {
                return new DataResult<PlaceDto>(ResultStatus.Success, new PlaceDto
                {
                    Place = place,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<PlaceDto>(ResultStatus.Error, "Aradığınız mekan bulunamadı.", null);
        }

        public async Task<IDataResult<PlaceListDto>> GetAll()
        {
            var places = await _unitOfWork.Places.GetAllAsync(null,  a => a.Category, a => a.City);
            if (places.Count > -1)
            {
                return new DataResult<PlaceListDto>(ResultStatus.Success, new PlaceListDto
                {
                    Places = places,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PlaceListDto>(ResultStatus.Error, "Mekanlar bulunamadı.", null);
        }

        public async Task<IDataResult<PlaceListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var places = await _unitOfWork.Places.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive,  ar => ar.Category);
                if (places.Count > -1)
                {
                    return new DataResult<PlaceListDto>(ResultStatus.Success, new PlaceListDto
                    {
                        Places = places,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<PlaceListDto>(ResultStatus.Error, "Mekanlar bulunamadı.", null);
            }
            return new DataResult<PlaceListDto>(ResultStatus.Error, "Aradığınız kategori bulunamadı.", null);
        }

        public async Task<IDataResult<PlaceListDto>> GetAllByCity(int cityId)
        {
            var result = await _unitOfWork.Cities.AnyAsync(c => c.Id == cityId);
            if (result)
            {
                var places = await _unitOfWork.Places.GetAllAsync(a => a.CityId == cityId && !a.IsDeleted && a.IsActive, ar => ar.City);
                if (places.Count > -1)
                {
                    return new DataResult<PlaceListDto>(ResultStatus.Success, new PlaceListDto
                    {
                        Places = places,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<PlaceListDto>(ResultStatus.Error, "Mekanlar bulunamadı.", null);
            }
            return new DataResult<PlaceListDto>(ResultStatus.Error, "Aradığınız şehir bulunamadı.", null);
        }

        public  async Task<IDataResult<PlaceListDto>> GetAllByNonDeleted()
        {
            var places = await _unitOfWork.Places.GetAllAsync(a => !a.IsDeleted, ar => ar.Category, ar => ar.City);
            if (places.Count > -1)
            {
                return new DataResult<PlaceListDto>(ResultStatus.Success, new PlaceListDto
                {
                    Places = places,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PlaceListDto>(ResultStatus.Error, "Mekanlar bulunamadı.", null);
        }

        public async Task<IDataResult<PlaceListDto>> GetAllByNonDeletedAndActive()
        {
            var places = await _unitOfWork.Places.GetAllAsync(a => !a.IsDeleted && a.IsActive, ar => ar.Category, ar => ar.City);
            if (places.Count > -1)
            {
                return new DataResult<PlaceListDto>(ResultStatus.Success, new PlaceListDto
                {
                    Places = places,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PlaceListDto>(ResultStatus.Error, "Mekanlar bulunamadı.", null);
        }

        public async Task<IResult> HardDelete(int placeId)
        {
            var result = await _unitOfWork.Places.AnyAsync(a => a.Id == placeId);
            if (result)
            {
                var place = await _unitOfWork.Places.GetAsync(a => a.Id == placeId);
                await _unitOfWork.Places.DeleteAsync(place).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{place.Name} isimli mekan başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir mekan bulunamadı.");
        }

        public async Task<IResult> Update(PlaceUpdateDto placeUpdateDto, string modifiedByName)
        {
            var place = _mapper.Map<Place>(placeUpdateDto);
            place.ModifiedByName = modifiedByName;
            await _unitOfWork.Places.UpdateAsync(place).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{placeUpdateDto.Name} isimli mekan başarıyla güncellenmiştir.");
        }
    }
}
