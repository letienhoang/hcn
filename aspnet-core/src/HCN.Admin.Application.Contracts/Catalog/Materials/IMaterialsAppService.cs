using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.Materials
{
    public interface IMaterialsAppService : ICrudAppService
        <MaterialDto,
        Guid, PagedResultRequestDto,
        CreateUpdateMaterialDto,
        CreateUpdateMaterialDto>
    {
        Task<PagedResultDto<MaterialInListDto>> GetListFilterAsync(MaterialListFilterDto input);

        Task<List<MaterialInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<string> GetThumbnailImageAsync(string fileName);
    }
}