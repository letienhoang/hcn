using HCN.Admin;
using HCN.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace HCN.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HCNEntityFrameworkCoreModule),
    typeof(AdminApplicationContractsModule)
    )]
public class HCNDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
