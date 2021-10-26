using ITG.Entities.Concrete;
using ITG.Entities.Dtos;
using ITG.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Services.Abstract
{
    public interface ICityService
    {
        Task<IDataResult<CityDto>> Get(int cityId);
        Task<IDataResult<CityListDto>> GetAll();
        Task<IDataResult<CityListDto>> GetAllByNonDeleted();
        Task<IDataResult<CityListDto>> GetAllByNonDeletedAndActive();
        Task<IResult> Add(CityAddDto cityAddDto, string createdByName);
        Task<IResult> Update(CityUpdateDto cityUpdateDto, string modifiedByName);
        Task<IResult> Delete(int cityId, string modifiedByName);
        Task<IResult> HardDelete(int cityId);
    }
}
