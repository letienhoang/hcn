using System;

namespace HCN.Admin.Catalog.Formulas
{
    public class FormulaListFilterDto : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}