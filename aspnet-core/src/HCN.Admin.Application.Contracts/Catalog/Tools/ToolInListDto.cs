using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Tools
{
    public class ToolInListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ThumbnailPicture { get; set; }
        public bool Visibility { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
    }
}