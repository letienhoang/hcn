using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.MaterialCategories
{
    public interface IMaterialCategoriesAppService : ICrudAppService
        <MaterialCategoryDto,
        Guid, PagedResultRequestDto,
        CreateUpdateMaterialCategoryDto,
        CreateUpdateMaterialCategoryDto>
    {
        Task<PagedResultDto<MaterialCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input);

        Task<List<MaterialCategoryInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<string> GetThumbnailImageAsync(string fileName);
    }
}