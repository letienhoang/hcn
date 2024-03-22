using HCN.Admin.Catalog.Formulas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.FormulaCategories
{
    public interface IFormulaCategoriesAppService : ICrudAppService
        <FormulaCategoryDto,
        Guid, PagedResultRequestDto,
        CreateUpdateFormulaCategoryDto,
        CreateUpdateFormulaCategoryDto>
    {
        Task<PagedResultDto<FormulaCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input);

        Task<List<FormulaCategoryInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<string> GetThumbnailImageAsync(string fileName);

        public Task<FormulaCategoryDto> UpdateVisibilityAsync(Guid formulaCategoryId, bool visibility);
    }
}