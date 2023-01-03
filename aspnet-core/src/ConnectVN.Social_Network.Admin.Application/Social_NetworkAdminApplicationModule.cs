using ConnectVN.Social_Network.Common.Domain;
using ConnectVN.Social_Network.Common.Settings;
using System;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Minio;
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
    typeof(AbpBlobStoringMinioModule)

    )]
public class Social_NetworkAdminApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<Social_NetworkAdminApplicationModule>();
        });
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseMinio(minio =>
                {
                    minio.EndPoint = Environment.GetEnvironmentVariable(Social_NetworkSettings.ENV_ENDPOINT);
                    minio.AccessKey = Environment.GetEnvironmentVariable(Social_NetworkSettings.ENV_ACCESSKEY);
                    minio.SecretKey = Environment.GetEnvironmentVariable(Social_NetworkSettings.ENV_SECRETKEY);
                    minio.BucketName = Environment.GetEnvironmentVariable(Social_NetworkSettings.ENV_BUCKETNAME);
                });
            });
        });
    }
}
