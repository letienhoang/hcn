using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.Tags
{
    public interface ITagsAppService : ICrudAppService
        <TagDto,
        Guid, PagedResultRequestDto,
        CreateUpdateTagDto,
        CreateUpdateTagDto>
    {
        Task<PagedResultDto<TagInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<TagInListDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
    }
}