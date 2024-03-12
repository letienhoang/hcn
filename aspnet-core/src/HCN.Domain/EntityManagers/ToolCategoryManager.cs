using System;
using System.Threading.Tasks;
using HCN.Tools;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class ToolCategoryManager : DomainService
    {
        private readonly IRepository<ToolCategory, Guid> _toolCategoryRepository;

        public ToolCategoryManager(IRepository<ToolCategory, Guid> toolCategoryRepository)
        {
            _toolCategoryRepository = toolCategoryRepository;
        }

        public async Task<ToolCategory> CreateAsync(string name, string slug,
            string description, bool visibility,
            string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            if (await _toolCategoryRepository.AnyAsync(x => x.Name == name))
            {
                throw new UserFriendlyException("Tên danh mục đã tồn tại", HCNDomainErrorCodes.ToolCategoryNameAlreadyExists);
            }

            return new ToolCategory(Guid.NewGuid(), name, slug, null, description, keywordSEO, descriptionSEO, parentId, visibility);
        }
    }
}