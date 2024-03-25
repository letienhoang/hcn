using HCN.Admin.Catalog.Tags;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.Units
{
    public interface IUnitsAppService : ICrudAppService
        <UnitDto,
        Guid, PagedResultRequestDto,
        CreateUpdateUnitDto,
        CreateUpdateUnitDto>
    {
        Task<PagedResultDto<UnitInListDto>> GetListFilterAsync(BaseListFilterDto input);

        Task<List<UnitInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        public Task<UnitDto> UpdateVisibilityAsync(Guid tagId, bool visibility);
    }
}