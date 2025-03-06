using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Tags
{
    public class TagStory : Entity
    {
        public TagStory()
        { }

        public Guid StoryId { get; set; }
        public Guid TagId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { StoryId, TagId };
        }
    }
}