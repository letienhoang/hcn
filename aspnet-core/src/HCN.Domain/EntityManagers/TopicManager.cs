﻿using System;
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
            if (await _topicRepository.AnyAsync(x => x.Name.ToUpper() == name.ToUpper()))
            {
                throw new UserFriendlyException("Tên chủ đề đã tồn tại", HCNDomainErrorCodes.TopicNameAlreadyExists);
            }
            if (await _topicRepository.AnyAsync(x => x.Code.ToUpper() == code.ToUpper()))
            {
                throw new UserFriendlyException("Mã chủ đề đã tồn tại", HCNDomainErrorCodes.TopicCodeAlreadyExists);
            }

            return new Topic(Guid.NewGuid(), name, slug, code, null, description, keywordSEO, descriptionSEO, parentId, visibility);
        }

        public async Task<Topic> GetUpdateAsync(Guid id, string name, string code)
        {
            if (await _topicRepository.AnyAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Id != id))
            {
                throw new UserFriendlyException("Tên chủ đề đã tồn tại", HCNDomainErrorCodes.TopicNameAlreadyExists);
            }

            if (await _topicRepository.AnyAsync(x => x.Code.ToUpper() == code.ToUpper() && x.Id != id))
            {
                throw new UserFriendlyException("Mã chủ đề đã tồn tại", HCNDomainErrorCodes.TopicCodeAlreadyExists);
            }

            return await _topicRepository.GetAsync(id);
        }
    }
}