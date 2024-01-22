using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Tags
{
    public class TagTool : Entity
    {
        public TagTool()
        { }

        public Guid ToolId { get; set; }
        public Guid TagId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { ToolId, TagId };
        }
    }
}