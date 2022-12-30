using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ConnectVN.Social_Network.Common.Domain.Data;

/* This is used if database provider does't define
 * ISocial_NetworkDbSchemaMigrator implementation.
 */
public class NullSocial_NetworkDbSchemaMigrator : ISocial_NetworkDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
