using System;
using System.Threading.Tasks;
using HCN.Stories;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class TopicManager : DomainService
    {
        private readonly IRepository<Topic, Guid> _topicRepository;

        public TopicManager(IRepository<Topic, Guid> topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<Topic> CreateAsync(string name, string slug,
            string code, string description, bool visibility,
            string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            if (await _topicRepository.AnyAsync(x => x.Name == name))
            {
                throw new UserFriendlyException("Tên chủ đề đã tồn tại", HCNDomainErrorCodes.TopicNameAlreadyExists);
            }
            if (await _topicRepository.AnyAsync(x => x.Code == code))
            {
                throw new UserFriendlyException("Mã chủ đề đã tồn tại", HCNDomainErrorCodes.TopicCodeAlreadyExists);
            }

            return new Topic(Guid.NewGuid(), name, slug, code, null, description, keywordSEO, descriptionSEO, parentId, visibility);
        }
    }
}