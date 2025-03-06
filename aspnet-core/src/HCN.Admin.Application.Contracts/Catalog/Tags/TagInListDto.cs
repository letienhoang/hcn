using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Tags
{
    public class TagInListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Visibility { get; set; }
    }
}