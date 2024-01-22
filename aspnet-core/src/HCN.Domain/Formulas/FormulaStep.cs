using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Formulas
{
    public class FormulaStep : Entity<Guid>
    {
        public FormulaStep()
        { }

        public Guid FormulaId { get; set; }
        public string Title { get; set; }
        public string Pictures { get; set; }
        public string Content { get; set; }
        public int SortOrder { get; set; }
    }
}