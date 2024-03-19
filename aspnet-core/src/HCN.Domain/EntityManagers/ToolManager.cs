using HCN.Tools;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class ToolManager : DomainService
    {
        private readonly IRepository<Tool, Guid> _toolRepository;

        public ToolManager(IRepository<Tool, Guid> toolRepository)
        {
            _toolRepository = toolRepository;
        }

        public async Task<Tool> CreateAsync(string name, string slug, string code,
            Guid categoryId, ToolType toolType, string description, 
            string pictures, bool visibility, string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            if (await _toolRepository.AnyAsync(x => x.Name == name))
            {
                throw new UserFriendlyException("Tên công cụ đã tồn tại", HCNDomainErrorCodes.ToolNameAlreadyExists);
            }
            if (await _toolRepository.AnyAsync(x => x.Code == code))
            {
                throw new UserFriendlyException("Mã công cụ đã tồn tại", HCNDomainErrorCodes.ToolCodeAlreadyExists);
            }

            return new Tool(Guid.NewGuid(), name, slug, code, categoryId, toolType, description,
                                null, pictures, visibility, keywordSEO, descriptionSEO, parentId);
        }

        public async Task<Tool> GetUpdateAsync(Guid id, string name, string code)
        {
            if (await _toolRepository.AnyAsync(x => x.Name == name && x.Id != id))
            {
                throw new UserFriendlyException("Tên công cụ đã tồn tại", HCNDomainErrorCodes.ToolNameAlreadyExists);
            }

            if (await _toolRepository.AnyAsync(x => x.Code == code && x.Id != id))
            {
                throw new UserFriendlyException("Mã công cụ đã tồn tại", HCNDomainErrorCodes.ToolCodeAlreadyExists);
            }

            return await _toolRepository.GetAsync(id);
        }
    }
}