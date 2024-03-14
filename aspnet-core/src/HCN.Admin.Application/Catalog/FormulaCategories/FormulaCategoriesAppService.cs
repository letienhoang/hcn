using HCN.Admin.Permissions;
using HCN.BlobContainers;
using HCN.EntityManagers;
using HCN.Formulas;
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

namespace HCN.Admin.Catalog.FormulaCategories
{
    [Authorize(AdminPermissions.FormulaCategory.Default, Policy = "AdminOnly")]
    public class FormulaCategoriesAppService : CrudAppService
        <FormulaCategory,
        FormulaCategoryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateFormulaCategoryDto,
        CreateUpdateFormulaCategoryDto>, IFormulaCategoriesAppService
    {
        private readonly IBlobContainer<FormulaCategoryCoverPictureContainer> _blobContainer;
        private readonly FormulaCategoryManager _formulaCategoryManager;

        public FormulaCategoriesAppService(IRepository<FormulaCategory, Guid> repository,
            IBlobContainer<FormulaCategoryCoverPictureContainer> blobContainer,
            FormulaCategoryManager formulaCategoryManager
            )
            : base(repository)
        {
            _blobContainer = blobContainer;
            _formulaCategoryManager = formulaCategoryManager;

            GetPolicyName = AdminPermissions.FormulaCategory.Default;
            GetListPolicyName = AdminPermissions.FormulaCategory.Default;
            CreatePolicyName = AdminPermissions.FormulaCategory.Create;
            UpdatePolicyName = AdminPermissions.FormulaCategory.Update;
            DeletePolicyName = AdminPermissions.FormulaCategory.Delete;
        }

        [Authorize(AdminPermissions.FormulaCategory.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var formulaCategory = await Repository.GetAsync(id);
                if(!formulaCategory.CoverPicture.IsNullOrEmpty())
                {
                    await _blobContainer.DeleteAsync(formulaCategory.CoverPicture);
                }
            }
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.FormulaCategory.Default)]
        public async Task<List<FormulaCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<FormulaCategory>, List<FormulaCategoryInListDto>>(data);
        }

        [Authorize(AdminPermissions.FormulaCategory.Default)]
        public async Task<PagedResultDto<FormulaCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<FormulaCategoryInListDto>(totalCount, ObjectMapper.Map<List<FormulaCategory>, List<FormulaCategoryInListDto>>(data));
        }

        [Authorize(AdminPermissions.FormulaCategory.Update)]
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _blobContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(AdminPermissions.FormulaCategory.Create)]
        public override async Task<FormulaCategoryDto> CreateAsync(CreateUpdateFormulaCategoryDto input)
        {
            var formulaCategory = await _formulaCategoryManager.CreateAsync(input.Name, input.Slug, input.Description, input.Visibility,
            input.KeywordSEO, input.DescriptionSEO, input.ParentId);
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.FormulaCategoryIdentitySettingPrefix + formulaCategory.Id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                formulaCategory.CoverPicture = coverPictureName;
            }
            var result = await Repository.InsertAsync(formulaCategory);
            return ObjectMapper.Map<FormulaCategory, FormulaCategoryDto>(result);
        }

        [Authorize(AdminPermissions.FormulaCategory.Update)]
        public override async Task<FormulaCategoryDto> UpdateAsync(Guid id, CreateUpdateFormulaCategoryDto input)
        {
            var formulaCategory = await _formulaCategoryManager.GetUpdateAsync(id, input.Name);
            if (formulaCategory == null)
                throw new BusinessException(HCNDomainErrorCodes.FormulaCategoryIsNotExists);
            formulaCategory.Name = input.Name;
            formulaCategory.Slug = input.Slug;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.FormulaCategoryIdentitySettingPrefix + id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                formulaCategory.CoverPicture = coverPictureName;
            }
            formulaCategory.Description = input.Description;
            formulaCategory.Visibility = input.Visibility;
            formulaCategory.KeywordSEO = input.KeywordSEO;
            formulaCategory.DescriptionSEO = input.DescriptionSEO;
            formulaCategory.ParentId = input.ParentId;
            await Repository.UpdateAsync(formulaCategory);

            return ObjectMapper.Map<FormulaCategory, FormulaCategoryDto>(formulaCategory);
        }

        [Authorize(AdminPermissions.FormulaCategory.Default)]
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