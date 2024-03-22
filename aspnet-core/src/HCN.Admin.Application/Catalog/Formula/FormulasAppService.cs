using HCN.Admin.Catalog.Tags;
using HCN.Admin.Permissions;
using HCN.BlobContainers;
using HCN.EntityManagers;
using HCN.Helpers;
using HCN.Formulas;
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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Xml.Linq;

namespace HCN.Admin.Catalog.Formulas
{
    [Authorize(AdminPermissions.Formula.Default, Policy = "AdminOnly")]
    public class FormulasAppService : CrudAppService
        <Formula,
        FormulaDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateFormulaDto,
        CreateUpdateFormulaDto>, IFormulasAppService
    {
        private readonly IBlobContainer<FormulaCoverPictureContainer> _blobContainer;
        private readonly FormulaManager _formulaManager;
        private readonly CodeGenerators _codeGenerators;
        private readonly SlugBuilder _slugBuilder;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<TagFormula> _tagFormulaRepository;

        public FormulasAppService(IRepository<Formula, Guid> repository,
            IBlobContainer<FormulaCoverPictureContainer> blobContainer,
            FormulaManager formulaManager,
            CodeGenerators codeGenerators,
            SlugBuilder slugBuilder,
            IRepository<Tag> tagRepository,
            IRepository<TagFormula> tagFormulaRepository)
            : base(repository)
        {
            _blobContainer = blobContainer;
            _formulaManager = formulaManager;
            _codeGenerators = codeGenerators;
            _slugBuilder = slugBuilder;
            _tagRepository = tagRepository;
            _tagFormulaRepository = tagFormulaRepository;

            GetPolicyName = AdminPermissions.Formula.Default;
            GetListPolicyName = AdminPermissions.Formula.Default;
            CreatePolicyName = AdminPermissions.Formula.Create;
            UpdatePolicyName = AdminPermissions.Formula.Update;
            DeletePolicyName = AdminPermissions.Formula.Delete;
        }

        [Authorize(AdminPermissions.Formula.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var formula = await Repository.GetAsync(id);
                if (!formula.ThumbnailPicture.IsNullOrEmpty())
                {
                    await _blobContainer.DeleteAsync(formula.ThumbnailPicture);
                }
                var tagOlds = await _tagFormulaRepository.GetListAsync(x => x.FormulaId == formula.Id);
                await _tagFormulaRepository.DeleteManyAsync(tagOlds);
            }
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.Formula.Default)]
        public async Task<List<FormulaInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Formula>, List<FormulaInListDto>>(data);
        }

        [Authorize(AdminPermissions.Formula.Default)]
        public async Task<PagedResultDto<FormulaInListDto>> GetListFilterAsync(FormulaListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
            query = query.WhereIf(input.CategoryId.HasValue, x => x.CategoryId == input.CategoryId);

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.OrderByDescending(x => x.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<FormulaInListDto>(totalCount, ObjectMapper.Map<List<Formula>, List<FormulaInListDto>>(data));
        }

        [Authorize(AdminPermissions.Formula.Update)]
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _blobContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }

        [Authorize(AdminPermissions.Formula.Create)]
        public override async Task<FormulaDto> CreateAsync(CreateUpdateFormulaDto input)
        {
            var formula = await _formulaManager.CreateAsync(input.Name, input.Slug, input.Code, input.CategoryId, input.Level, 
                input.FormulaType, input.ExecutionTime, input.BriefContent, input.Description, input.Liked, input.ViewCount, input.SortOrder, input.Visibility, 
                input.VideoUrl, input.ReferenceSource, input.KeywordSEO, input.DescriptionSEO, input.ParentId);
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.FormulaIdentitySettingPrefix + formula.Id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                formula.ThumbnailPicture = coverPictureName;
            }

            var result = await Repository.InsertAsync(formula);
            return ObjectMapper.Map<Formula, FormulaDto>(result);
        }

        [Authorize(AdminPermissions.Formula.Update)]
        public override async Task<FormulaDto> UpdateAsync(Guid id, CreateUpdateFormulaDto input)
        {
            var formula = await _formulaManager.GetUpdateAsync(id, input.Name, input.Code) ?? throw new BusinessException(HCNDomainErrorCodes.FormulaIsNotExists);
            formula.Name = input.Name;
            formula.Slug = input.Slug;
            formula.Code = input.Code;
            formula.CategoryId = input.CategoryId;
            formula.Level = input.Level;
            formula.FormulaType = input.FormulaType;
            formula.ExecutionTime = input.ExecutionTime;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                string fileSubStr = input.CoverPictureName.Substring(input.CoverPictureName.LastIndexOf('.'));
                string coverPictureName = HCNConsts.FormulaIdentitySettingPrefix + id.ToString() + fileSubStr;
                await SaveThumbnailImageAsync(coverPictureName, input.CoverPictureContent);
                formula.ThumbnailPicture = coverPictureName;
            }
            formula.BriefContent = input.BriefContent;
            formula.Description = input.Description;
            formula.Liked = input.Liked;
            formula.ViewCount = input.ViewCount;
            formula.SortOrder = input.SortOrder;
            formula.Visibility = input.Visibility;
            formula.VideoUrl = input.VideoUrl;
            formula.ReferenceSource = input.ReferenceSource;
            formula.KeywordSEO = input.KeywordSEO;
            formula.DescriptionSEO = input.DescriptionSEO;
            formula.ParentId = input.ParentId;

            await Repository.UpdateAsync(formula);

            return ObjectMapper.Map<Formula, FormulaDto>(formula);
        }

        [Authorize(AdminPermissions.Formula.Default)]
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
            return await _codeGenerators.FormulaGenerateAsync();
        }

        [Authorize(AdminPermissions.Formula.Default)]
        public async Task<List<TagInListDto>> GetFormulaTagAsync(Guid formulaId)
        {
            var formulaTagList = await _tagFormulaRepository.GetListAsync(x => x.FormulaId == formulaId);
            var tagtagList = new List<Tag>();

            if (formulaTagList is null)
            {
                return null;
            }
            else
            {
                foreach (var formulaTag in formulaTagList)
                {
                    var tag = await _tagRepository.FirstOrDefaultAsync(x => x.Id == formulaTag.TagId);
                    if (tag is not null)
                    {
                        tagtagList.Add(tag);
                    }
                }
            }

            return ObjectMapper.Map<List<Tag>, List<TagInListDto>>(tagtagList);
        }

        [Authorize(AdminPermissions.Formula.Update)]
        public async Task<FormulaDto> UpdateFormulaTagAsync(Guid formulaId, string[] formulaTagList)
        {
            var formula = await Repository.GetAsync(formulaId);
            if (!formulaTagList.IsNullOrEmpty())
            {
                var tagOlds = await _tagFormulaRepository.GetListAsync(x => x.FormulaId == formulaId);
                if (tagOlds.Count > 0) await _tagFormulaRepository.DeleteManyAsync(tagOlds);

                foreach (var tagName in formulaTagList)
                {
                    var tagAsync = await _tagRepository.FirstOrDefaultAsync(x => x.Name.ToUpper() == tagName.ToUpper());
                    if (tagAsync == null)
                    {
                        var tagSlug = _slugBuilder.GetSlug(tagName).Result;
                        var tag = new Tag(Guid.NewGuid(), tagName, tagSlug, true);
                        await _tagRepository.InsertAsync(tag);
                        var tagFormula = new TagFormula()
                        {
                            FormulaId = formulaId,
                            TagId = tag.Id
                        };
                        await _tagFormulaRepository.InsertAsync(tagFormula);
                    }
                    else
                    {
                        var tagFormula = new TagFormula()
                        {
                            FormulaId = formulaId,
                            TagId = tagAsync.Id
                        };
                        await _tagFormulaRepository.InsertAsync(tagFormula);
                    }
                }
            }

            return ObjectMapper.Map<Formula, FormulaDto>(formula);
        }

        [Authorize(AdminPermissions.Formula.Update)]
        public async Task<FormulaDto> UpdateVisibilityAsync(Guid formulaId, bool visibility)
        {
            var formula = await Repository.GetAsync(formulaId);
            formula.Visibility = visibility;
            await Repository.UpdateAsync(formula);

            return ObjectMapper.Map<Formula, FormulaDto>(formula);
        }
    }
}