using HCN.Admin.Catalog.Tags;
using HCN.Admin.Permissions;
using HCN.BlobContainers;
using HCN.EntityManagers;
using HCN.Helpers;
using HCN.Stories;
using HCN.Tags;
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

namespace HCN.Admin.Catalog.Stories
{
    [Authorize(AdminPermissions.Story.Default, Policy = "AdminOnly")]
    public class StoriesAppService : CrudAppService
        <Story,
        StoryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateStoryDto,
        CreateUpdateStoryDto>, IStoriesAppService
    {
        private readonly IBlobContainer<StoryCoverPictureContainer> _blobContainer;
        private readonly StoryManager _storyManager;
        private readonly CodeGenerators _codeGenerators;
        private readonly SlugBuilder _slugBuilder;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<TagStory> _tagStoryRepository;

        public StoriesAppService(IRepository<Story, Guid> repository,
            IBlobContainer<StoryCoverPictureContainer> blobContainer,
            StoryManager storyManager,
            CodeGenerators codeGenerators,
            SlugBuilder slugBuilder,
            IRepository<Tag> tagRepository,
            IRepository<TagStory> tagStoryRepository
            )
            : base(repository)
        {
            _blobContainer = blobContainer;
            _storyManager = storyManager;
            _codeGenerators = codeGenerators;
            _slugBuilder = slugBuilder;
            _tagRepository = tagRepository;
            _tagStoryRepository = tagStoryRepository;

            GetPolicyName = AdminPermissions.Story.Default;
            GetListPolicyName = AdminPermissions.Story.Default;
            CreatePolicyName = AdminPermissions.Story.Create;
            UpdatePolicyName = AdminPermissions.Story.Update;
            DeletePolicyName = AdminPermissions.Story.Delete;
        }

        [Authorize(AdminPermissions.Story.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var story = await Repository.GetAsync(id);
                if (!story.ThumbnailPicture.IsNullOrEmpty())
                {
                    await _blobContainer.DeleteAsync(story.ThumbnailPicture);
                }
                var tagOlds = await _tagStoryRepository.GetListAsync(x => x.StoryId == story.Id);
                await _tagStoryRepository.DeleteManyAsync(tagOlds);
            }
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.Story.Default)]
        public async Task<List<StoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Story>, List<StoryInListDto>>(data);
        }

        [Authorize(AdminPermissions.Story.Default)]
        public async Task<PagedResultDto<StoryInListDto>> GetListFilterAsync(StoryListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Title.Contains(input.Keyword));
            query = query.WhereIf(input.TopicId.HasValue, x => x.TopicId == input.TopicId);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.OrderByDescending(x => x.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<StoryInListDto>(totalCount, ObjectMapper.Map<List<Story>, List<StoryInListDto>>(data));
        }

        [Authorize(AdminPermissions.Story.Update)]
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _blobContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(AdminPermissions.Story.Create)]
        public override async Task<StoryDto> CreateAsync(CreateUpdateStoryDto input)
        {
            var story = await _storyManager.CreateAsync(input.Title, input.Slug, input.Code, input.BriefContent, input.Content,
            input.Pictures, input.Liked, input.ViewCount, input.SortOrder, input.Visibility,
            input.ReferenceSource, input.KeywordSEO, input.DescriptionSEO, input.TopicId);
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.StoryIdentitySettingPrefix + story.Id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                story.ThumbnailPicture = coverPictureName;
            }

            var result = await Repository.InsertAsync(story);
            return ObjectMapper.Map<Story, StoryDto>(result);
        }

        [Authorize(AdminPermissions.Story.Update)]
        public override async Task<StoryDto> UpdateAsync(Guid id, CreateUpdateStoryDto input)
        {
            var story = await _storyManager.GetUpdateAsync(id, input.Title, input.Code) ?? throw new BusinessException(HCNDomainErrorCodes.StoryIsNotExists);
            story.Title = input.Title;
            story.Slug = input.Slug;
            story.Code = input.Code;
            story.BriefContent = input.BriefContent;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.StoryIdentitySettingPrefix + id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                story.ThumbnailPicture = coverPictureName;
            }
            story.Pictures = input.Pictures;
            story.Liked = input.Liked;
            story.ViewCount = input.ViewCount;
            story.SortOrder = input.SortOrder;
            story.Visibility = input.Visibility;
            story.ReferenceSource = input.ReferenceSource;
            story.KeywordSEO = input.KeywordSEO;
            story.DescriptionSEO = input.DescriptionSEO;
            story.TopicId = input.TopicId;

            await Repository.UpdateAsync(story);

            return ObjectMapper.Map<Story, StoryDto>(story);
        }

        [Authorize(AdminPermissions.Story.Default)]
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
            return await _codeGenerators.StoryGenerateAsync();
        }

        [Authorize(AdminPermissions.Story.Default)]
        public async Task<List<TagInListDto>> GetStoryTagAsync(Guid storyId)
        {
            var storyTagList = await _tagStoryRepository.GetListAsync(x => x.StoryId == storyId);
            var tagtagList = new List<Tag>();

            if (storyTagList is null)
            {
                return null;
            }
            else
            {
                foreach (var storyTag in storyTagList)
                {
                    var tag = await _tagRepository.FirstOrDefaultAsync(x => x.Id == storyTag.TagId);
                    if (tag is not null)
                    {
                        tagtagList.Add(tag);
                    }
                }
            }

            return ObjectMapper.Map<List<Tag>, List<TagInListDto>>(tagtagList);
        }

        [Authorize(AdminPermissions.Story.Update)]
        public async Task<StoryDto> UpdateStoryTagAsync(Guid storyId, string[] storyTagList)
        {
            var story = await Repository.GetAsync(storyId);
            if (!storyTagList.IsNullOrEmpty())
            {
                var tagOlds = await _tagStoryRepository.GetListAsync(x => x.StoryId == storyId);
                if (tagOlds.Count > 0) await _tagStoryRepository.DeleteManyAsync(tagOlds);

                foreach (var tagName in storyTagList)
                {
                    var tagAsync = await _tagRepository.FirstOrDefaultAsync(x => x.Name.ToUpper() == tagName.ToUpper());
                    if (tagAsync == null)
                    {
                        var tagSlug = _slugBuilder.GetSlug(tagName).Result;
                        var tag = new Tag(Guid.NewGuid(), tagName, tagSlug, true);
                        await _tagRepository.InsertAsync(tag);
                        var tagStory = new TagStory()
                        {
                            StoryId = storyId,
                            TagId = tag.Id
                        };
                        await _tagStoryRepository.InsertAsync(tagStory);
                    }
                    else
                    {
                        var tagStory = new TagStory()
                        {
                            StoryId = storyId,
                            TagId = tagAsync.Id
                        };
                        await _tagStoryRepository.InsertAsync(tagStory);
                    }
                }
            }

            return ObjectMapper.Map<Story, StoryDto>(story);
        }
    }
}