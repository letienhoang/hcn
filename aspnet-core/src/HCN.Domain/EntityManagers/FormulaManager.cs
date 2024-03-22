using HCN.Formulas;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class FormulaManager : DomainService
    {
        private readonly IRepository<Formula, Guid> _formulaRepository;

        public FormulaManager(IRepository<Formula, Guid> formulaRepository)
        {
            _formulaRepository = formulaRepository;
        }

        public async Task<Formula> CreateAsync(string name, string slug, string code, Guid categoryId,
            Level level, FormulaType formulaType, int executionTime, string briefContent, string description, 
            int liked, int viewCount, int sortOrder, bool visibility, string videoUrl, string referenceSource, 
            string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            if (await _formulaRepository.AnyAsync(x => x.Name.ToUpper() == name.ToUpper()))
            {
                throw new UserFriendlyException("Tên công thức đã tồn tại", HCNDomainErrorCodes.FormulaNameAlreadyExists);
            }
            if (await _formulaRepository.AnyAsync(x => x.Code.ToUpper() == code.ToUpper()))
            {
                throw new UserFriendlyException("Mã công thức đã tồn tại", HCNDomainErrorCodes.FormulaCodeAlreadyExists);
            }

            return new Formula(Guid.NewGuid(), name, slug, code, categoryId, level, formulaType, executionTime,
                null, briefContent, description, liked, viewCount, sortOrder, visibility, videoUrl, referenceSource,
                keywordSEO, descriptionSEO, parentId);
        }

        public async Task<Formula> GetUpdateAsync(Guid id, string name, string code)
        {
            if (await _formulaRepository.AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Id != id))
            {
                throw new UserFriendlyException("Tên công thức đã tồn tại", HCNDomainErrorCodes.FormulaNameAlreadyExists);
            }

            if (await _formulaRepository.AnyAsync(x => x.Code.ToUpper() == code.ToUpper() && x.Id != id))
            {
                throw new UserFriendlyException("Mã công thức đã tồn tại", HCNDomainErrorCodes.FormulaCodeAlreadyExists);
            }

            return await _formulaRepository.GetAsync(id);
        }
    }
}