using HCN.Admin.Permissions;
using HCN.BlobContainers;
using HCN.EntityManagers;
using HCN.Materials;
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

namespace HCN.Admin.Catalog.MaterialCategories
{
    [Authorize(AdminPermissions.MaterialCategory.Default, Policy = "AdminOnly")]
    public class MaterialCategoriesAppService : CrudAppService
        <MaterialCategory,
        MaterialCategoryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateMaterialCategoryDto,
        CreateUpdateMaterialCategoryDto>, IMaterialCategoriesAppService
    {
        private readonly IBlobContainer<MaterialCategoryCoverPictureContainer> _blobContainer;
        private readonly MaterialCategoryManager _materialCategoryManager;

        public MaterialCategoriesAppService(IRepository<MaterialCategory, Guid> repository,
            IBlobContainer<MaterialCategoryCoverPictureContainer> blobContainer,
            MaterialCategoryManager materialCategoryManager
            )
            : base(repository)
        {
            _blobContainer = blobContainer;
            _materialCategoryManager = materialCategoryManager;

            GetPolicyName = AdminPermissions.MaterialCategory.Default;
            GetListPolicyName = AdminPermissions.MaterialCategory.Default;
            CreatePolicyName = AdminPermissions.MaterialCategory.Create;
            UpdatePolicyName = AdminPermissions.MaterialCategory.Update;
            DeletePolicyName = AdminPermissions.MaterialCategory.Delete;
        }

        [Authorize(AdminPermissions.MaterialCategory.Delete)]
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

        [Authorize(AdminPermissions.MaterialCategory.Default)]
        public async Task<List<MaterialCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<MaterialCategory>, List<MaterialCategoryInListDto>>(data);
        }

        [Authorize(AdminPermissions.MaterialCategory.Default)]
        public async Task<PagedResultDto<MaterialCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<MaterialCategoryInListDto>(totalCount, ObjectMapper.Map<List<MaterialCategory>, List<MaterialCategoryInListDto>>(data));
        }

        [Authorize(AdminPermissions.MaterialCategory.Update)]
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _blobContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(AdminPermissions.MaterialCategory.Create)]
        public override async Task<MaterialCategoryDto> CreateAsync(CreateUpdateMaterialCategoryDto input)
        {
            var MaterialCategory = await _materialCategoryManager.CreateAsync(input.Name, input.Slug, input.Description, input.Visibility,
            input.KeywordSEO, input.DescriptionSEO, input.ParentId);
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.MaterialCategoryIdentitySettingPrefix + MaterialCategory.Id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                MaterialCategory.CoverPicture = coverPictureName;
            }
            var result = await Repository.InsertAsync(MaterialCategory);
            return ObjectMapper.Map<MaterialCategory, MaterialCategoryDto>(result);
        }

        [Authorize(AdminPermissions.MaterialCategory.Update)]
        public override async Task<MaterialCategoryDto> UpdateAsync(Guid id, CreateUpdateMaterialCategoryDto input)
        {
            var MaterialCategory = await _materialCategoryManager.GetUpdateAsync(id, input.Name);
            if (MaterialCategory == null)
                throw new BusinessException(HCNDomainErrorCodes.MaterialCategoryIsNotExists);
            MaterialCategory.Name = input.Name;
            MaterialCategory.Slug = input.Slug;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.MaterialCategoryIdentitySettingPrefix + id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                MaterialCategory.CoverPicture = coverPictureName;
            }
            MaterialCategory.Description = input.Description;
            MaterialCategory.Visibility = input.Visibility;
            MaterialCategory.KeywordSEO = input.KeywordSEO;
            MaterialCategory.DescriptionSEO = input.DescriptionSEO;
            MaterialCategory.ParentId = input.ParentId;
            await Repository.UpdateAsync(MaterialCategory);

            return ObjectMapper.Map<MaterialCategory, MaterialCategoryDto>(MaterialCategory);
        }

        [Authorize(AdminPermissions.MaterialCategory.Default)]
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