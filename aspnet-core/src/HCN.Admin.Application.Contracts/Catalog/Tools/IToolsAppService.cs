using HCN.Admin.Catalog.Tags;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.Tools
{
    public interface IToolsAppService : ICrudAppService
        <ToolDto,
        Guid, PagedResultRequestDto,
        CreateUpdateToolDto,
        CreateUpdateToolDto>
    {
        Task<PagedResultDto<ToolInListDto>> GetListFilterAsync(ToolListFilterDto input);

        Task<List<ToolInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<string> GetThumbnailImageAsync(string fileName);

        public Task<List<TagInListDto>> GetToolTagAsync(Guid toolId);

        public Task<ToolDto> UpdateToolTagAsync(Guid toolId, string[] toolTagList);

        public Task<ToolDto> UpdateVisibilityAsync(Guid toolId, bool visibility);
    }
}