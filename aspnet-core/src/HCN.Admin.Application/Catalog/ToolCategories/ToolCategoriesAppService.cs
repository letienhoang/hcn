using HCN.Admin.Permissions;
using HCN.BlobContainers;
using HCN.Tools;
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

namespace HCN.Admin.Catalog.ToolCategories
{
    [Authorize(AdminPermissions.ToolCategory.Default, Policy = "AdminOnly")]
    public class ToolCategoriesAppService : CrudAppService
        <ToolCategory,
        ToolCategoryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateToolCategoryDto,
        CreateUpdateToolCategoryDto>, IToolCategoriesAppService
    {
        private readonly IBlobContainer<ToolCategoryCoverPictureContainer> _blobContainer;
        private readonly ToolCategoryManager _materialCategoryManager;

        public ToolCategoriesAppService(IRepository<ToolCategory, Guid> repository,
            IBlobContainer<ToolCategoryCoverPictureContainer> blobContainer,
            ToolCategoryManager materialCategoryManager
            )
            : base(repository)
        {
            _blobContainer = blobContainer;
            _materialCategoryManager = materialCategoryManager;

            GetPolicyName = AdminPermissions.ToolCategory.Default;
            GetListPolicyName = AdminPermissions.ToolCategory.Default;
            CreatePolicyName = AdminPermissions.ToolCategory.Create;
            UpdatePolicyName = AdminPermissions.ToolCategory.Update;
            DeletePolicyName = AdminPermissions.ToolCategory.Delete;
        }

        [Authorize(AdminPermissions.ToolCategory.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var materialCategory = await Repository.GetAsync(id);
                if (!materialCategory.CoverPicture.IsNullOrEmpty())
                {
                    await _blobContainer.DeleteAsync(materialCategory.CoverPicture);
                }
            }
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.ToolCategory.Default)]
        public async Task<List<ToolCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ToolCategory>, List<ToolCategoryInListDto>>(data);
        }

        [Authorize(AdminPermissions.ToolCategory.Default)]
        public async Task<PagedResultDto<ToolCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ToolCategoryInListDto>(totalCount, ObjectMapper.Map<List<ToolCategory>, List<ToolCategoryInListDto>>(data));
        }

        [Authorize(AdminPermissions.ToolCategory.Update)]
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _blobContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(AdminPermissions.ToolCategory.Create)]
        public override async Task<ToolCategoryDto> CreateAsync(CreateUpdateToolCategoryDto input)
        {
            var ToolCategory = await _materialCategoryManager.CreateAsync(input.Name, input.Slug, input.Description, input.Visibility,
            input.KeywordSEO, input.DescriptionSEO, input.ParentId);
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.ToolCategoryIdentitySettingPrefix + ToolCategory.Id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                ToolCategory.CoverPicture = coverPictureName;
            }
            var result = await Repository.InsertAsync(ToolCategory);
            return ObjectMapper.Map<ToolCategory, ToolCategoryDto>(result);
        }

        [Authorize(AdminPermissions.ToolCategory.Update)]
        public override async Task<ToolCategoryDto> UpdateAsync(Guid id, CreateUpdateToolCategoryDto input)
        {
            var ToolCategory = await Repository.GetAsync(id);
            if (ToolCategory == null)
                throw new BusinessException(HCNDomainErrorCodes.ToolCategoryIsNotExists);
            ToolCategory.Name = input.Name;
            ToolCategory.Slug = input.Slug;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.ToolCategoryIdentitySettingPrefix + id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                ToolCategory.CoverPicture = coverPictureName;
            }
            ToolCategory.Description = input.Description;
            ToolCategory.Visibility = input.Visibility;
            ToolCategory.KeywordSEO = input.KeywordSEO;
            ToolCategory.DescriptionSEO = input.DescriptionSEO;
            ToolCategory.ParentId = input.ParentId;
            await Repository.UpdateAsync(ToolCategory);

            return ObjectMapper.Map<ToolCategory, ToolCategoryDto>(ToolCategory);
        }

        [Authorize(AdminPermissions.ToolCategory.Default)]
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
    }
}