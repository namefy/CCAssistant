using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.OpenIddict;

namespace Ezreal.CCAssistant
{
    public class AbpDefaultOpenIddictClaimsPrincipalHandler : IAbpOpenIddictClaimsPrincipalHandler, ITransientDependency
    {
        public Task HandleAsync(AbpOpenIddictClaimsPrincipalHandlerContext context)
        {
            TimeSpan expires = new TimeSpan(1, 0, 0, 0, 0);
            context.Principal.SetAccessTokenLifetime(expires);
            return Task.CompletedTask;
        }
    }
}
