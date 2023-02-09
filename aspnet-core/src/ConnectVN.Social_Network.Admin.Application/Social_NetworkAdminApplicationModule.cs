using ConnectVN.Social_Network.Common.Domain;
using ConnectVN.Social_Network.Common.Settings;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Azure;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace ConnectVN.Social_Network.Admin;

[DependsOn(
    typeof(Social_NetworkDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(Social_NetworkAdminApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBlobStoringAzureModule)
    )]
public class Social_NetworkAdminApplicationModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<Social_NetworkAdminApplicationModule>();
        });
        var configuration = context.Services.GetConfiguration();

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseAzure(azure =>
                {
                    azure.ConnectionString = configuration.GetSection($"Application:{Social_NetworkSettings.ENV_CONNECTIONSTRING}").Value;
                    azure.ContainerName = configuration.GetSection($"Application:{Social_NetworkSettings.ENV_CONTAINERNAME}").Value;
                });
            });
        });
    }
}
