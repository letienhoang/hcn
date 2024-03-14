using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Tags
{
    public class TagDto : IEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Visibility { get; set; }
        public Guid Id { get; set; }
    }
}