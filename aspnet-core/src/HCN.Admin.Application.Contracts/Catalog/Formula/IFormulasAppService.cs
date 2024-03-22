using HCN.Admin.Catalog.Tags;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.Formulas
{
    public interface IFormulasAppService : ICrudAppService
        <FormulaDto,
        Guid, PagedResultRequestDto,
        CreateUpdateFormulaDto,
        CreateUpdateFormulaDto>
    {
        Task<PagedResultDto<FormulaInListDto>> GetListFilterAsync(FormulaListFilterDto input);

        Task<List<FormulaInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<string> GetThumbnailImageAsync(string fileName);

        public Task<List<TagInListDto>> GetFormulaTagAsync(Guid formulaId);

        public Task<FormulaDto> UpdateFormulaTagAsync(Guid formulaId, string[] formulaTagList);

        public Task<FormulaDto> UpdateVisibilityAsync(Guid formulaId, bool visibility);
    }
}