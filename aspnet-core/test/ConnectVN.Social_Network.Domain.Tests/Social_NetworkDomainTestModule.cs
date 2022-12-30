using ConnectVN.Social_Network.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ConnectVN.Social_Network;

[DependsOn(
    typeof(Social_NetworkEntityFrameworkCoreTestModule)
    )]
public class Social_NetworkDomainTestModule : AbpModule
{

}
