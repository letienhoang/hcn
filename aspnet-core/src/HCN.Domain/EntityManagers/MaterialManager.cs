using HCN.Materials;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class MaterialManager : DomainService
    {
        private readonly IRepository<Material, Guid> _storyRepository;

        public MaterialManager(IRepository<Material, Guid> storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<Material> CreateAsync(string name, string slug, string code,
            Guid categoryId, MaterialType materialType, string description, 
            string pictures, bool visibility, string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            if (await _storyRepository.AnyAsync(x => x.Name == name))
            {
                throw new UserFriendlyException("Tên nguyên liệu đã tồn tại", HCNDomainErrorCodes.MaterialNameAlreadyExists);
            }
            if (await _storyRepository.AnyAsync(x => x.Code == code))
            {
                throw new UserFriendlyException("Mã nguyên liệu đã tồn tại", HCNDomainErrorCodes.MaterialCodeAlreadyExists);
            }

            return new Material(Guid.NewGuid(), name, slug, code, categoryId, materialType, description,
                                null, pictures, visibility, keywordSEO, descriptionSEO, parentId);
        }

        public async Task<Material> GetUpdateAsync(Guid id, string name, string code)
        {
            if (await _storyRepository.AnyAsync(x => x.Name == name && x.Id != id))
            {
                throw new UserFriendlyException("Tên nguyên liệu đã tồn tại", HCNDomainErrorCodes.MaterialNameAlreadyExists);
            }

            if (await _storyRepository.AnyAsync(x => x.Code == code))
            {
                throw new UserFriendlyException("Mã nguyên liệu đã tồn tại", HCNDomainErrorCodes.MaterialCodeAlreadyExists);
            }

            return await _storyRepository.GetAsync(id);
        }
    }
}