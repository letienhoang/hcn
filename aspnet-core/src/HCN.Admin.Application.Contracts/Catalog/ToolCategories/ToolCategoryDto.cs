using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.ToolCategories
{
    public class ToolCategoryDto : IEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string CoverPicture { get; set; }
        public string Description { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? ParentId { get; set; }
        public bool Visibility { get; set; }
        public Guid Id { get; set; }
    }
}