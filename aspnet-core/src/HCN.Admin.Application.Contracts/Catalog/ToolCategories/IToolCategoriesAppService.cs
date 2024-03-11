using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.ToolCategories
{
    public interface IToolCategoriesAppService : ICrudAppService
        <ToolCategoryDto,
        Guid, PagedResultRequestDto,
        CreateUpdateToolCategoryDto,
        CreateUpdateToolCategoryDto>
    {
        Task<PagedResultDto<ToolCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input);

        Task<List<ToolCategoryInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<string> GetThumbnailImageAsync(string fileName);
    }
}