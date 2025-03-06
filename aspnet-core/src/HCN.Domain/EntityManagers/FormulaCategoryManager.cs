using System;
using System.Threading.Tasks;
using HCN.Formulas;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class FormulaCategoryManager : DomainService
    {
        private readonly IRepository<FormulaCategory, Guid> _formulaCategoryRepository;

        public FormulaCategoryManager(IRepository<FormulaCategory, Guid> formulaCategoryRepository)
        {
            _formulaCategoryRepository = formulaCategoryRepository;
        }

        public async Task<FormulaCategory> CreateAsync(string name, string slug,
            string description, bool visibility,
            string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            if (await _formulaCategoryRepository.AnyAsync(x => x.Name.ToUpper() == name.ToUpper()))
            {
                throw new UserFriendlyException("Tên danh mục đã tồn tại", HCNDomainErrorCodes.FormulaCategoryNameAlreadyExists);
            }

            return new FormulaCategory(Guid.NewGuid(), name, slug, null, description, visibility, keywordSEO, descriptionSEO, parentId);
        }

        public async Task<FormulaCategory> GetUpdateAsync(Guid id, string name)
        {
            if (await _formulaCategoryRepository.AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Id != id))
            {
                throw new UserFriendlyException("Tên danh mục đã tồn tại", HCNDomainErrorCodes.FormulaCategoryNameAlreadyExists);
            }

            return await _formulaCategoryRepository.GetAsync(id);
        }
    }
}