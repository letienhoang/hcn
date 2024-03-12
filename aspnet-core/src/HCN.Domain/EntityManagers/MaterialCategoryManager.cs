using System;
using System.Threading.Tasks;
using HCN.Materials;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class MaterialCategoryManager : DomainService
    {
        private readonly IRepository<MaterialCategory, Guid> _materialCategoryRepository;

        public MaterialCategoryManager(IRepository<MaterialCategory, Guid> materialCategoryRepository)
        {
            _materialCategoryRepository = materialCategoryRepository;
        }

        public async Task<MaterialCategory> CreateAsync(string name, string slug,
            string description, bool visibility,
            string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            if (await _materialCategoryRepository.AnyAsync(x => x.Name == name))
            {
                throw new UserFriendlyException("Tên danh mục đã tồn tại", HCNDomainErrorCodes.MaterialCategoryNameAlreadyExists);
            }

            return new MaterialCategory(Guid.NewGuid(), name, slug, null, description, keywordSEO, descriptionSEO, parentId, visibility);
        }
    }
}