using HCN.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HCN;

[DependsOn(
    typeof(HCNEntityFrameworkCoreTestModule)
    )]
public class HCNDomainTestModule : AbpModule
{

}
