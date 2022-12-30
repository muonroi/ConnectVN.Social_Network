using System.Threading.Tasks;

namespace ConnectVN.Social_Network.Common.Domain.Data;

public interface ISocial_NetworkDbSchemaMigrator
{
    Task MigrateAsync();
}
