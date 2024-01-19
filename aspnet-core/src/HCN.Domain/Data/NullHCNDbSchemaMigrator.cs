using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HCN.Data;

/* This is used if database provider does't define
 * IHCNDbSchemaMigrator implementation.
 */
public class NullHCNDbSchemaMigrator : IHCNDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
