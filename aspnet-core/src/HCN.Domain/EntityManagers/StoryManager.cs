using System;
using System.Threading.Tasks;
using HCN.Stories;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HCN.EntityManagers
{
    public class StoryManager : DomainService
    {
        private readonly IRepository<Story, Guid> _storyRepository;

        public StoryManager(IRepository<Story, Guid> storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<Story> CreateAsync(string title, string slug, string code, string briefContent, string content,
            string pictures, int liked, int viewCount, int sortOrder, bool visibility,
            string referenceSource, string keywordSEO, string descriptionSEO, Guid? topicId)
        {
            if (await _storyRepository.AnyAsync(x => x.Title == title))
            {
                throw new UserFriendlyException("Tiêu đề câu chuyện đã tồn tại", HCNDomainErrorCodes.StoryNameAlreadyExists);
            }
            if (await _storyRepository.AnyAsync(x => x.Code == code))
            {
                throw new UserFriendlyException("Mã câu chuyện đã tồn tại", HCNDomainErrorCodes.StoryCodeAlreadyExists);
            }

            return new Story(Guid.NewGuid(), title, slug, code, briefContent, content,
            null, pictures, liked, viewCount, sortOrder, visibility,
            referenceSource, keywordSEO, descriptionSEO, topicId);
        }

        public async Task<Story> GetUpdateAsync(Guid id, string title, string code)
        {
            if (await _storyRepository.AnyAsync(x => x.Title == title && x.Id != id))
            {
                throw new UserFriendlyException("Tiêu đề câu chuyện đã tồn tại", HCNDomainErrorCodes.StoryNameAlreadyExists);
            }

            if (await _storyRepository.AnyAsync(x => x.Code == code && x.Id != id))
            {
                throw new UserFriendlyException("Mã câu chuyện đã tồn tại", HCNDomainErrorCodes.StoryCodeAlreadyExists);
            }

            return await _storyRepository.GetAsync(id);
        }
    }
}