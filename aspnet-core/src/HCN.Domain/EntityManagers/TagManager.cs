using HCN.Tags;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class TagManager : DomainService
    {
        private readonly IRepository<Tag, Guid> _toolCategoryRepository;

        public TagManager(IRepository<Tag, Guid> toolCategoryRepository)
        {
            _toolCategoryRepository = toolCategoryRepository;
        }

        public async Task<Tag> CreateAsync(string name, string slug, bool visibility)
        {
            if (await _toolCategoryRepository.AnyAsync(x => x.Name == name))
            {
                throw new UserFriendlyException("Tên thẻ đã tồn tại", HCNDomainErrorCodes.TagNameAlreadyExists);
            }

            return new Tag(Guid.NewGuid(), name, slug, visibility);
        }

        public async Task<Tag> GetUpdateAsync(Guid id, string name)
        {
            if (await _toolCategoryRepository.AnyAsync(x => x.Name == name && x.Id != id))
            {
                throw new UserFriendlyException("Tên thẻ đã tồn tại", HCNDomainErrorCodes.TagNameAlreadyExists);
            }

            return await _toolCategoryRepository.GetAsync(id);
        }
    }
}