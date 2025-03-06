using System;

namespace HCN.Admin.Catalog.MaterialCategories
{
    public class CreateUpdateMaterialCategoryDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? ParentId { get; set; }
        public bool Visibility { get; set; }
        public string CoverPictureName { get; set; }
        public string CoverPictureContent { get; set; }
    }
}