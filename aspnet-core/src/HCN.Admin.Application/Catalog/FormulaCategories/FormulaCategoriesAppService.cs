using HCN.Admin.Permissions;
using HCN.Formulas;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
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
        public FormulaCategoriesAppService(IRepository<FormulaCategory, Guid> repository)
            : base(repository)
        {
            GetPolicyName = AdminPermissions.FormulaCategory.Default;
            GetListPolicyName = AdminPermissions.FormulaCategory.Default;
            CreatePolicyName = AdminPermissions.FormulaCategory.Create;
            UpdatePolicyName = AdminPermissions.FormulaCategory.Update;
            DeletePolicyName = AdminPermissions.FormulaCategory.Delete;
        }

        [Authorize(AdminPermissions.FormulaCategory.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
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
    }
}