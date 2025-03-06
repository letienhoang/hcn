using HCN.Admin.Permissions;
using HCN.BlobContainers;
using HCN.EntityManagers;
using HCN.Stories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;

namespace HCN.Admin.Catalog.Topics
{
    [Authorize(AdminPermissions.Topic.Default, Policy = "AdminOnly")]
    public class TopicsAppService : CrudAppService
        <Topic,
        TopicDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTopicDto,
        CreateUpdateTopicDto>, ITopicsAppService
    {
        private readonly IBlobContainer<TopicCoverPictureContainer> _blobContainer;
        private readonly TopicManager _topicManager;
        private readonly CodeGenerators _codeGenerators;

        public TopicsAppService(IRepository<Topic, Guid> repository,
            IBlobContainer<TopicCoverPictureContainer> blobContainer,
            TopicManager topicManager,
            CodeGenerators codeGenerators
            )
            : base(repository)
        {
            _blobContainer = blobContainer;
            _topicManager = topicManager;
            _codeGenerators = codeGenerators;

            GetPolicyName = AdminPermissions.Topic.Default;
            GetListPolicyName = AdminPermissions.Topic.Default;
            CreatePolicyName = AdminPermissions.Topic.Create;
            UpdatePolicyName = AdminPermissions.Topic.Update;
            DeletePolicyName = AdminPermissions.Topic.Delete;
        }

        [Authorize(AdminPermissions.Topic.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var topic = await Repository.GetAsync(id);
                if (!topic.CoverPicture.IsNullOrEmpty())
                {
                    await _blobContainer.DeleteAsync(topic.CoverPicture);
                }
            }
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.Topic.Default)]
        public async Task<List<TopicInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Topic>, List<TopicInListDto>>(data);
        }

        [Authorize(AdminPermissions.Topic.Default)]
        public async Task<PagedResultDto<TopicInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<TopicInListDto>(totalCount, ObjectMapper.Map<List<Topic>, List<TopicInListDto>>(data));
        }

        [Authorize(AdminPermissions.Topic.Update)]
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _blobContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(AdminPermissions.Topic.Create)]
        public override async Task<TopicDto> CreateAsync(CreateUpdateTopicDto input)
        {
            var topic = await _topicManager.CreateAsync(input.Name, input.Slug, input.Code, input.Description, input.Visibility,
            input.KeywordSEO, input.DescriptionSEO, input.ParentId);
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.TopicIdentitySettingPrefix + topic.Id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                topic.CoverPicture = coverPictureName;
            }
            var result = await Repository.InsertAsync(topic);
            return ObjectMapper.Map<Topic, TopicDto>(result);
        }

        [Authorize(AdminPermissions.Topic.Update)]
        public override async Task<TopicDto> UpdateAsync(Guid id, CreateUpdateTopicDto input)
        {
            var topic = await _topicManager.GetUpdateAsync(id, input.Name, input.Code);
            if (topic == null)
                throw new BusinessException(HCNDomainErrorCodes.TopicIsNotExists);
            topic.Name = input.Name;
            topic.Slug = input.Slug;
            topic.Code = input.Code;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.TopicIdentitySettingPrefix + id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                topic.CoverPicture = coverPictureName;
            }
            topic.Description = input.Description;
            topic.Visibility = input.Visibility;
            topic.KeywordSEO = input.KeywordSEO;
            topic.DescriptionSEO = input.DescriptionSEO;
            topic.ParentId = input.ParentId;
            await Repository.UpdateAsync(topic);

            return ObjectMapper.Map<Topic, TopicDto>(topic);
        }

        [Authorize(AdminPermissions.Topic.Default)]
        public async Task<string> GetThumbnailImageAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var thumbnailContent = await _blobContainer.GetAllBytesOrNullAsync(fileName);

            if (thumbnailContent is null)
            {
                return null;
            }
            var result = Convert.ToBase64String(thumbnailContent);
            return result;
        }

        public async Task<string> GetSuggestNewCodeAsync()
        {
            return await _codeGenerators.TopicGenerateAsync();
        }

        [Authorize(AdminPermissions.Topic.Update)]
        public async Task<TopicDto> UpdateVisibilityAsync(Guid topicId, bool visibility)
        {
            var topic = await Repository.GetAsync(topicId);
            topic.Visibility = visibility;
            await Repository.UpdateAsync(topic);

            return ObjectMapper.Map<Topic, TopicDto>(topic);
        }
    }
}