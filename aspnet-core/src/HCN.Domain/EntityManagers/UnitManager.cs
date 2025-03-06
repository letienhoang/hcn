using HCN.Units;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class UnitManager : DomainService
    {
        private readonly IRepository<Unit, Guid> _unitCategoryRepository;

        public UnitManager(IRepository<Unit, Guid> unitCategoryRepository)
        {
            _unitCategoryRepository = unitCategoryRepository;
        }

        public async Task<Unit> CreateAsync(string name, UnitType unitType, string briefContent, bool visibility)
        {
            if (await _unitCategoryRepository.AnyAsync(x => x.Name.ToUpper() == name.ToUpper()))
            {
                throw new UserFriendlyException("Tên đơn vị đã tồn tại", HCNDomainErrorCodes.UnitNameAlreadyExists);
            }

            return new Unit(Guid.NewGuid(), name, unitType, briefContent, visibility);
        }

        public async Task<Unit> GetUpdateAsync(Guid id, string name)
        {
            if (await _unitCategoryRepository.AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Id != id))
            {
                throw new UserFriendlyException("Tên đơn vị tồn tại", HCNDomainErrorCodes.UnitNameAlreadyExists);
            }

            return await _unitCategoryRepository.GetAsync(id);
        }
    }
}