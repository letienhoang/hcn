using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Formulas
{
    public class FormulaInListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ThumbnailPicture { get; set; }
        public int Liked { get; set; }
        public int ViewCount { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
    }
}