using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.System.Users
{
    public class UserInListDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}