using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ezreal.CCAssistant.Data;

/* This is used if database provider does't define
 * ICCAssistantDbSchemaMigrator implementation.
 */
public class NullCCAssistantDbSchemaMigrator : ICCAssistantDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
