using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.Stories
{
    public interface IStoriesAppService : ICrudAppService
        <StoryDto,
        Guid, PagedResultRequestDto,
        CreateUpdateStoryDto,
        CreateUpdateStoryDto>
    {
        Task<PagedResultDto<StoryInListDto>> GetListFilterAsync(StoryListFilterDto input);

        Task<List<StoryInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<string> GetThumbnailImageAsync(string fileName);
    }
}