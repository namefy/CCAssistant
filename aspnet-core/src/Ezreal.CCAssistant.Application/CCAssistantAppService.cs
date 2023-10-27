using System;
using System.Collections.Generic;
using System.Text;
using Ezreal.CCAssistant.Localization;
using Volo.Abp.Application.Services;

namespace Ezreal.CCAssistant;

/* Inherit your application services from this class.
 */
public abstract class CCAssistantAppService : ApplicationService
{
    protected CCAssistantAppService()
    {
        LocalizationResource = typeof(CCAssistantResource);
    }
}
