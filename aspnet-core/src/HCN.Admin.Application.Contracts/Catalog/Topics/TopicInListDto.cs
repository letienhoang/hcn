using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Topics
{
    public class TopicInListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public string CoverPicture { get; set; }
        public bool Visibility { get; set; }
    }
}