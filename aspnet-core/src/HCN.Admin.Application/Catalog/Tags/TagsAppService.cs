using HCN.Admin.Permissions;
using HCN.EntityManagers;
using HCN.Tags;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HCN.Admin.Catalog.Tags
{
    [Authorize(AdminPermissions.Tag.Default, Policy = "AdminOnly")]
    public class TagsAppService : CrudAppService
        <Tag,
        TagDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTagDto,
        CreateUpdateTagDto>, ITagsAppService
    {
        private readonly TagManager _tagManager;

        public TagsAppService(IRepository<Tag, Guid> repository,
            TagManager tagManager
            )
            : base(repository)
        {
            _tagManager = tagManager;

            GetPolicyName = AdminPermissions.Tag.Default;
            GetListPolicyName = AdminPermissions.Tag.Default;
            CreatePolicyName = AdminPermissions.Tag.Create;
            UpdatePolicyName = AdminPermissions.Tag.Update;
            DeletePolicyName = AdminPermissions.Tag.Delete;
        }

        [Authorize(AdminPermissions.Tag.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(AdminPermissions.Tag.Default)]
        public async Task<List<TagInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Visibility == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Tag>, List<TagInListDto>>(data);
        }

        [Authorize(AdminPermissions.Tag.Default)]
        public async Task<PagedResultDto<TagInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<TagInListDto>(totalCount, ObjectMapper.Map<List<Tag>, List<TagInListDto>>(data));
        }

        [Authorize(AdminPermissions.Tag.Create)]
        public override async Task<TagDto> CreateAsync(CreateUpdateTagDto input)
        {
            var tag = await _tagManager.CreateAsync(input.Name, input.Slug, input.Visibility);
            var result = await Repository.InsertAsync(tag);
            return ObjectMapper.Map<Tag, TagDto>(result);
        }

        [Authorize(AdminPermissions.Tag.Update)]
        public override async Task<TagDto> UpdateAsync(Guid id, CreateUpdateTagDto input)
        {
            var tag = await _tagManager.GetUpdateAsync(id, input.Name);
            if (tag == null)
                throw new BusinessException(HCNDomainErrorCodes.TagIsNotExists);
            tag.Name = input.Name;
            tag.Slug = input.Slug;
            tag.Visibility = input.Visibility;
            await Repository.UpdateAsync(tag);

            return ObjectMapper.Map<Tag, TagDto>(tag);
        }

        [Authorize(AdminPermissions.Tag.Update)]
        public async Task<TagDto> UpdateVisibilityAsync(Guid tagId, bool visibility)
        {
            var tag = await Repository.GetAsync(tagId);
            tag.Visibility = visibility;
            await Repository.UpdateAsync(tag);

            return ObjectMapper.Map<Tag, TagDto>(tag);
        }
    }
}