using HCN.Admin.Catalog.Tags;
using HCN.Admin.Permissions;
using HCN.BlobContainers;
using HCN.EntityManagers;
using HCN.Helpers;
using HCN.Tools;
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

namespace HCN.Admin.Catalog.Tools
{
    [Authorize(AdminPermissions.Tool.Default, Policy = "AdminOnly")]
    public class ToolsAppService : CrudAppService
        <Tool,
        ToolDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateToolDto,
        CreateUpdateToolDto>, IToolsAppService
    {
        private readonly IBlobContainer<ToolCoverPictureContainer> _blobContainer;
        private readonly ToolManager _toolManager;
        private readonly CodeGenerators _codeGenerators;
        private readonly SlugBuilder _slugBuilder;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<TagTool> _tagToolRepository;

        public ToolsAppService(IRepository<Tool, Guid> repository,
            IBlobContainer<ToolCoverPictureContainer> blobContainer,
            ToolManager toolManager,
            CodeGenerators codeGenerators,
            SlugBuilder slugBuilder,
            IRepository<Tag> tagRepository,
            IRepository<TagTool> tagToolRepository)
            : base(repository)
        {
            _blobContainer = blobContainer;
            _toolManager = toolManager;
            _codeGenerators = codeGenerators;
            _slugBuilder = slugBuilder;
            _tagRepository = tagRepository;
            _tagToolRepository = tagToolRepository;

            GetPolicyName = AdminPermissions.Tool.Default;
            GetListPolicyName = AdminPermissions.Tool.Default;
            CreatePolicyName = AdminPermissions.Tool.Create;
            UpdatePolicyName = AdminPermissions.Tool.Update;
            DeletePolicyName = AdminPermissions.Tool.Delete;
        }

        [Authorize(AdminPermissions.Tool.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var tool = await Repository.GetAsync(id);
                if (!tool.ThumbnailPicture.IsNullOrEmpty())
                {
                    await _blobContainer.DeleteAsync(tool.ThumbnailPicture);
                }
                var tagOlds = await _tagToolRepository.GetListAsync(x => x.ToolId == tool.Id);
                await _tagToolRepository.DeleteManyAsync(tagOlds);
            }
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.Tool.Default)]
        public async Task<List<ToolInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Tool>, List<ToolInListDto>>(data);
        }

        [Authorize(AdminPermissions.Tool.Default)]
        public async Task<PagedResultDto<ToolInListDto>> GetListFilterAsync(ToolListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
            query = query.WhereIf(input.CategoryId.HasValue, x => x.CategoryId == input.CategoryId);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.OrderByDescending(x => x.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ToolInListDto>(totalCount, ObjectMapper.Map<List<Tool>, List<ToolInListDto>>(data));
        }

        [Authorize(AdminPermissions.Tool.Update)]
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _blobContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(AdminPermissions.Tool.Create)]
        public override async Task<ToolDto> CreateAsync(CreateUpdateToolDto input)
        {
            var tool = await _toolManager.CreateAsync(input.Name, input.Slug, input.Code, input.CategoryId, input.ToolType, input.Description,
            input.Pictures, input.Visibility, input.KeywordSEO, input.DescriptionSEO, input.ParentId);
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.ToolIdentitySettingPrefix + tool.Id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                tool.ThumbnailPicture = coverPictureName;
            }

            var result = await Repository.InsertAsync(tool);
            return ObjectMapper.Map<Tool, ToolDto>(result);
        }

        [Authorize(AdminPermissions.Tool.Update)]
        public override async Task<ToolDto> UpdateAsync(Guid id, CreateUpdateToolDto input)
        {
            var tool = await _toolManager.GetUpdateAsync(id, input.Name, input.Code) ?? throw new BusinessException(HCNDomainErrorCodes.ToolIsNotExists);
            tool.Name = input.Name;
            tool.Slug = input.Slug;
            tool.Code = input.Code;
            tool.ToolType = input.ToolType;
            tool.Description = input.Description;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.ToolIdentitySettingPrefix + id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                tool.ThumbnailPicture = coverPictureName;
            }
            tool.Pictures = input.Pictures;
            tool.Visibility = input.Visibility;
            tool.KeywordSEO = input.KeywordSEO;
            tool.DescriptionSEO = input.DescriptionSEO;
            tool.ParentId = input.ParentId;

            await Repository.UpdateAsync(tool);

            return ObjectMapper.Map<Tool, ToolDto>(tool);
        }

        [Authorize(AdminPermissions.Tool.Default)]
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
            return await _codeGenerators.ToolGenerateAsync();
        }

        [Authorize(AdminPermissions.Tool.Default)]
        public async Task<List<TagInListDto>> GetToolTagAsync(Guid toolId)
        {
            var toolTagList = await _tagToolRepository.GetListAsync(x => x.ToolId == toolId);
            var tagtagList = new List<Tag>();

            if (toolTagList is null)
            {
                return null;
            }
            else
            {
                foreach (var toolTag in toolTagList)
                {
                    var tag = await _tagRepository.FirstOrDefaultAsync(x => x.Id == toolTag.TagId);
                    if (tag is not null)
                    {
                        tagtagList.Add(tag);
                    }
                }
            }

            return ObjectMapper.Map<List<Tag>, List<TagInListDto>>(tagtagList);
        }

        [Authorize(AdminPermissions.Tool.Update)]
        public async Task<ToolDto> UpdateToolTagAsync(Guid toolId, string[] toolTagList)
        {
            var tool = await Repository.GetAsync(toolId);
            if (!toolTagList.IsNullOrEmpty())
            {
                var tagOlds = await _tagToolRepository.GetListAsync(x => x.ToolId == toolId);
                if (tagOlds.Count > 0) await _tagToolRepository.DeleteManyAsync(tagOlds);

                foreach (var tagName in toolTagList)
                {
                    var tagAsync = await _tagRepository.FirstOrDefaultAsync(x => x.Name.ToUpper() == tagName.ToUpper());
                    if (tagAsync == null)
                    {
                        var tagSlug = _slugBuilder.GetSlug(tagName).Result;
                        var tag = new Tag(Guid.NewGuid(), tagName, tagSlug, true);
                        await _tagRepository.InsertAsync(tag);
                        var tagTool = new TagTool()
                        {
                            ToolId = toolId,
                            TagId = tag.Id
                        };
                        await _tagToolRepository.InsertAsync(tagTool);
                    }
                    else
                    {
                        var tagTool = new TagTool()
                        {
                            ToolId = toolId,
                            TagId = tagAsync.Id
                        };
                        await _tagToolRepository.InsertAsync(tagTool);
                    }
                }
            }

            return ObjectMapper.Map<Tool, ToolDto>(tool);
        }

        [Authorize(AdminPermissions.Tool.Update)]
        public async Task<ToolDto> UpdateVisibilityAsync(Guid toolId, bool visibility)
        {
            var tool = await Repository.GetAsync(toolId);
            tool.Visibility = visibility;
            await Repository.UpdateAsync(tool);

            return ObjectMapper.Map<Tool, ToolDto>(tool);
        }
    }
}