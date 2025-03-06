using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HCN.Admin.Catalog.Topics
{
    public interface ITopicsAppService : ICrudAppService
        <TopicDto,
        Guid, PagedResultRequestDto,
        CreateUpdateTopicDto,
        CreateUpdateTopicDto>
    {
        Task<PagedResultDto<TopicInListDto>> GetListFilterAsync(BaseListFilterDto input);

        Task<List<TopicInListDto>> GetListAllAsync();

        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<string> GetThumbnailImageAsync(string fileName);

        public Task<TopicDto> UpdateVisibilityAsync(Guid topicId, bool visibility);
    }
}