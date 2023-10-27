using System.Threading.Tasks;

namespace Ezreal.CCAssistant.Data;

public interface ICCAssistantDbSchemaMigrator
{
    Task MigrateAsync();
}
