using Ezreal.CCAssistant.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ezreal.CCAssistant;

[DependsOn(
    typeof(CCAssistantEntityFrameworkCoreTestModule)
    )]
public class CCAssistantDomainTestModule : AbpModule
{

}
