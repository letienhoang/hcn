using HCN.Admin.Permissions;
using HCN.EntityManagers;
using HCN.Units;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HCN.Admin.Catalog.Units
{
    [Authorize(AdminPermissions.Unit.Default, Policy = "AdminOnly")]
    public class UnitsAppService : CrudAppService
        <Unit,
        UnitDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateUnitDto,
        CreateUpdateUnitDto>, IUnitsAppService
    {
        private readonly UnitManager _unitManager;

        public UnitsAppService(IRepository<Unit, Guid> repository,
            UnitManager unitManager
            )
            : base(repository)
        {
            _unitManager = unitManager;

            GetPolicyName = AdminPermissions.Unit.Default;
            GetListPolicyName = AdminPermissions.Unit.Default;
            CreatePolicyName = AdminPermissions.Unit.Create;
            UpdatePolicyName = AdminPermissions.Unit.Update;
            DeletePolicyName = AdminPermissions.Unit.Delete;
        }

        [Authorize(AdminPermissions.Unit.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.Unit.Default)]
        public async Task<List<UnitInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Unit>, List<UnitInListDto>>(data);
        }

        [Authorize(AdminPermissions.Unit.Default)]
        public async Task<PagedResultDto<UnitInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<UnitInListDto>(totalCount, ObjectMapper.Map<List<Unit>, List<UnitInListDto>>(data));
        }

        [Authorize(AdminPermissions.Unit.Create)]
        public override async Task<UnitDto> CreateAsync(CreateUpdateUnitDto input)
        {
            var unit = await _unitManager.CreateAsync(input.Name, input.UnitType, input.BriefContent, input.Visibility);
            var result = await Repository.InsertAsync(unit);
            return ObjectMapper.Map<Unit, UnitDto>(result);
        }

        [Authorize(AdminPermissions.Unit.Update)]
        public override async Task<UnitDto> UpdateAsync(Guid id, CreateUpdateUnitDto input)
        {
            var unit = await _unitManager.GetUpdateAsync(id, input.Name);
            if (unit == null)
                throw new BusinessException(HCNDomainErrorCodes.UnitIsNotExists);
            unit.Name = input.Name;
            unit.UnitType = input.UnitType;
            unit.BriefContent = input.BriefContent;
            unit.Visibility = input.Visibility;
            await Repository.UpdateAsync(unit);

            return ObjectMapper.Map<Unit, UnitDto>(unit);
        }

        [Authorize(AdminPermissions.Unit.Update)]
        public async Task<UnitDto> UpdateVisibilityAsync(Guid unitId, bool visibility)
        {
            var unit = await Repository.GetAsync(unitId);
            unit.Visibility = visibility;
            await Repository.UpdateAsync(unit);

            return ObjectMapper.Map<Unit, UnitDto>(unit);
        }
    }
}