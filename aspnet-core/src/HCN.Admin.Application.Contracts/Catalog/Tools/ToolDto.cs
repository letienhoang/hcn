﻿using HCN.Tools;
using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Tools
{
    public class ToolDto : IEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public Guid CategoryId { get; set; }
        public ToolType ToolType { get; set; }
        public string Description { get; set; }
        public string ThumbnailPicture { get; set; }
        public string Pictures { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? ParentId { get; set; }
        public bool Visibility { get; set; }
        public Guid Id { get; set; }
    }
}