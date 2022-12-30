using ConnectVN.Social_Network.Common.EntityFrameworkCore;
using ConnectVN.Social_Network.Customer;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ConnectVN.Social_Network.Common.Infrastructure.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(Social_NetworkEntityFrameworkCoreModule),
    typeof(Social_NetworkCustomerApplicationContractsModule)
    )]
public class Social_NetworkDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
