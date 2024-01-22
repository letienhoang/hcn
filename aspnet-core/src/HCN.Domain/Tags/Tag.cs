using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Tags
{
    public class Tag : Entity<Guid>
    {
        public Tag()
        { }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}