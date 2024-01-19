using Volo.Abp.Modularity;

namespace HCN.Public;

[DependsOn(
    typeof(PublicApplicationModule),
    typeof(HCNDomainTestModule)
    )]
public class PublicApplicationTestModule : AbpModule
{

}
