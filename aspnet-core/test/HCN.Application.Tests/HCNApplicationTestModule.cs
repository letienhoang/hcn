using Volo.Abp.Modularity;

namespace HCN;

[DependsOn(
    typeof(HCNApplicationModule),
    typeof(HCNDomainTestModule)
    )]
public class HCNApplicationTestModule : AbpModule
{

}
