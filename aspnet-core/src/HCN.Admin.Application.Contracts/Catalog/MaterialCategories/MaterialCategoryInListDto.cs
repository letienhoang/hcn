using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.MaterialCategories
{
    public class MaterialCategoryInListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string CoverPicture { get; set; }
        public string Description { get; set; }
        public bool Visibility { get; set; }
    }
}