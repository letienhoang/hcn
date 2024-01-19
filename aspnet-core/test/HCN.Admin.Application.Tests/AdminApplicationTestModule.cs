using Volo.Abp.Modularity;

namespace HCN.Admin;

[DependsOn(
    typeof(AdminApplicationModule),
    typeof(HCNDomainTestModule)
    )]
public class AdminApplicationTestModule : AbpModule
{

}
