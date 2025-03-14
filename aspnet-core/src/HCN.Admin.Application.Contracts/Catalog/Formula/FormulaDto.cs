﻿using HCN.Formulas;
using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Formulas
{
    public class FormulaDto : IEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public Guid CategoryId { get; set; }
        public Level Level { get; set; }
        public FormulaType FormulaType { get; set; }
        public int ExecutionTime { get; set; }
        public string ThumbnailPicture { get; set; }
        public string BriefContent { get; set; }
        public string Description { get; set; }
        public int Liked { get; set; }
        public int ViewCount { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public string VideoUrl { get; set; }
        public string ReferenceSource { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
    }
}