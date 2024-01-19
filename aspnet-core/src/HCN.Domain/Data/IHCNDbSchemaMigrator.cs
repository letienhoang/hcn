using System.Threading.Tasks;

namespace HCN.Data;

public interface IHCNDbSchemaMigrator
{
    Task MigrateAsync();
}
