using Volo.Abp.Application.Dtos;

namespace HCN.Admin
{
    public class BaseListFilterDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}