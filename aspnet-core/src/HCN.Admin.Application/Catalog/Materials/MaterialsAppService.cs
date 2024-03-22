using HCN.Admin.Catalog.Tags;
using HCN.Admin.Permissions;
using HCN.BlobContainers;
using HCN.EntityManagers;
using HCN.Helpers;
using HCN.Materials;
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

namespace HCN.Admin.Catalog.Materials
{
    [Authorize(AdminPermissions.Material.Default, Policy = "AdminOnly")]
    public class MaterialsAppService : CrudAppService
        <Material,
        MaterialDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateMaterialDto,
        CreateUpdateMaterialDto>, IMaterialsAppService
    {
        private readonly IBlobContainer<MaterialCoverPictureContainer> _blobContainer;
        private readonly MaterialManager _materialManager;
        private readonly CodeGenerators _codeGenerators;
        private readonly SlugBuilder _slugBuilder;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<TagMaterial> _tagMaterialRepository;

        public MaterialsAppService(IRepository<Material, Guid> repository,
            IBlobContainer<MaterialCoverPictureContainer> blobContainer,
            MaterialManager materialManager,
            CodeGenerators codeGenerators,
            SlugBuilder slugBuilder,
            IRepository<Tag> tagRepository,
            IRepository<TagMaterial> tagMaterialRepository)
            : base(repository)
        {
            _blobContainer = blobContainer;
            _materialManager = materialManager;
            _codeGenerators = codeGenerators;
            _slugBuilder = slugBuilder;
            _tagRepository = tagRepository;
            _tagMaterialRepository = tagMaterialRepository;

            GetPolicyName = AdminPermissions.Material.Default;
            GetListPolicyName = AdminPermissions.Material.Default;
            CreatePolicyName = AdminPermissions.Material.Create;
            UpdatePolicyName = AdminPermissions.Material.Update;
            DeletePolicyName = AdminPermissions.Material.Delete;
        }

        [Authorize(AdminPermissions.Material.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var material = await Repository.GetAsync(id);
                if (!material.ThumbnailPicture.IsNullOrEmpty())
                {
                    await _blobContainer.DeleteAsync(material.ThumbnailPicture);
                }
                var tagOlds = await _tagMaterialRepository.GetListAsync(x => x.MaterialId == material.Id);
                await _tagMaterialRepository.DeleteManyAsync(tagOlds);
            }
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.Material.Default)]
        public async Task<List<MaterialInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Material>, List<MaterialInListDto>>(data);
        }

        [Authorize(AdminPermissions.Material.Default)]
        public async Task<PagedResultDto<MaterialInListDto>> GetListFilterAsync(MaterialListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
            query = query.WhereIf(input.CategoryId.HasValue, x => x.CategoryId == input.CategoryId);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.OrderByDescending(x => x.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<MaterialInListDto>(totalCount, ObjectMapper.Map<List<Material>, List<MaterialInListDto>>(data));
        }

        [Authorize(AdminPermissions.Material.Update)]
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _blobContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(AdminPermissions.Material.Create)]
        public override async Task<MaterialDto> CreateAsync(CreateUpdateMaterialDto input)
        {
            var material = await _materialManager.CreateAsync(input.Name, input.Slug, input.Code, input.CategoryId, input.MaterialType, input.Description,
            input.Pictures, input.Visibility, input.KeywordSEO, input.DescriptionSEO, input.ParentId);
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.MaterialIdentitySettingPrefix + material.Id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                material.ThumbnailPicture = coverPictureName;
            }

            var result = await Repository.InsertAsync(material);
            return ObjectMapper.Map<Material, MaterialDto>(result);
        }

        [Authorize(AdminPermissions.Material.Update)]
        public override async Task<MaterialDto> UpdateAsync(Guid id, CreateUpdateMaterialDto input)
        {
            var material = await _materialManager.GetUpdateAsync(id, input.Name, input.Code) ?? throw new BusinessException(HCNDomainErrorCodes.MaterialIsNotExists);
            material.Name = input.Name;
            material.Slug = input.Slug;
            material.Code = input.Code;
            material.MaterialType = input.MaterialType;
            material.Description = input.Description;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.MaterialIdentitySettingPrefix + id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                material.ThumbnailPicture = coverPictureName;
            }
            material.Pictures = input.Pictures;
            material.Visibility = input.Visibility;
            material.KeywordSEO = input.KeywordSEO;
            material.DescriptionSEO = input.DescriptionSEO;
            material.ParentId = input.ParentId;

            await Repository.UpdateAsync(material);

            return ObjectMapper.Map<Material, MaterialDto>(material);
        }

        [Authorize(AdminPermissions.Material.Default)]
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
            return await _codeGenerators.MaterialGenerateAsync();
        }

        [Authorize(AdminPermissions.Material.Default)]
        public async Task<List<TagInListDto>> GetMaterialTagAsync(Guid materialId)
        {
            var materialTagList = await _tagMaterialRepository.GetListAsync(x => x.MaterialId == materialId);
            var tagtagList = new List<Tag>();

            if (materialTagList is null)
            {
                return null;
            }
            else
            {
                foreach (var materialTag in materialTagList)
                {
                    var tag = await _tagRepository.FirstOrDefaultAsync(x => x.Id == materialTag.TagId);
                    if (tag is not null)
                    {
                        tagtagList.Add(tag);
                    }
                }
            }

            return ObjectMapper.Map<List<Tag>, List<TagInListDto>>(tagtagList);
        }

        [Authorize(AdminPermissions.Material.Update)]
        public async Task<MaterialDto> UpdateMaterialTagAsync(Guid materialId, string[] materialTagList)
        {
            var material = await Repository.GetAsync(materialId);
            if (!materialTagList.IsNullOrEmpty())
            {
                var tagOlds = await _tagMaterialRepository.GetListAsync(x => x.MaterialId == materialId);
                if (tagOlds.Count > 0) await _tagMaterialRepository.DeleteManyAsync(tagOlds);

                foreach (var tagName in materialTagList)
                {
                    var tagAsync = await _tagRepository.FirstOrDefaultAsync(x => x.Name.ToUpper() == tagName.ToUpper());
                    if (tagAsync == null)
                    {
                        var tagSlug = _slugBuilder.GetSlug(tagName).Result;
                        var tag = new Tag(Guid.NewGuid(), tagName, tagSlug, true);
                        await _tagRepository.InsertAsync(tag);
                        var tagMaterial = new TagMaterial()
                        {
                            MaterialId = materialId,
                            TagId = tag.Id
                        };
                        await _tagMaterialRepository.InsertAsync(tagMaterial);
                    }
                    else
                    {
                        var tagMaterial = new TagMaterial()
                        {
                            MaterialId = materialId,
                            TagId = tagAsync.Id
                        };
                        await _tagMaterialRepository.InsertAsync(tagMaterial);
                    }
                }
            }

            return ObjectMapper.Map<Material, MaterialDto>(material);
        }

        [Authorize(AdminPermissions.Material.Update)]
        public async Task<MaterialDto> UpdateVisibilityAsync(Guid materialId, bool visibility)
        {
            var material = await Repository.GetAsync(materialId);
            material.Visibility = visibility;
            await Repository.UpdateAsync(material);

            return ObjectMapper.Map<Material, MaterialDto>(material);
        }
    }
}