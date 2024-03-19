using System;

namespace HCN.Admin.Catalog.Tools
{
    public class ToolListFilterDto : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}