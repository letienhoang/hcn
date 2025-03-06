using System;

namespace HCN.Admin.Catalog.Materials
{
    public class MaterialListFilterDto : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}