using HCN.Admin.Catalog.Tags;
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

        public Task<List<TagInListDto>> GetStoryTagAsync(Guid storyId);

        public Task<StoryDto> UpdateStoryTagAsync(Guid storyId, string[] storyTagList);

        public Task<StoryDto> UpdateVisibilityAsync(Guid storyId, bool visibility);
    }
}