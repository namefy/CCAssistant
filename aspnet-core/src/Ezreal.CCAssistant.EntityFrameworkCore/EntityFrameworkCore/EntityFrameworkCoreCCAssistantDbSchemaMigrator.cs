using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ezreal.CCAssistant.Data;
using Volo.Abp.DependencyInjection;

namespace Ezreal.CCAssistant.EntityFrameworkCore;

public class EntityFrameworkCoreCCAssistantDbSchemaMigrator
    : ICCAssistantDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreCCAssistantDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the CCAssistantDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CCAssistantDbContext>()
            .Database
            .MigrateAsync();
    }
}
