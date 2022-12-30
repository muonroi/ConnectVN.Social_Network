using System;
using System.Threading.Tasks;
using ConnectVN.Social_Network.Common.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace ConnectVN.Social_Network.Common.EntityFrameworkCore;

public class EntityFrameworkCoreSocial_NetworkDbSchemaMigrator
    : ISocial_NetworkDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSocial_NetworkDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the Social_NetworkDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<Social_NetworkDbContext>()
            .Database
            .MigrateAsync();
    }
}
