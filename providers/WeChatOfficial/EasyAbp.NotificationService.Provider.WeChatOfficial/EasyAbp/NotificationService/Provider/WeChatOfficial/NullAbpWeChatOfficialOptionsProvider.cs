using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[Dependency(TryRegister = true)]
public class NullAbpWeChatOfficialOptionsProvider : IAbpWeChatOfficialOptionsProvider, ITransientDependency
{
    private readonly ILogger<NullAbpWeChatOfficialOptionsProvider> _logger;

    public NullAbpWeChatOfficialOptionsProvider(ILogger<NullAbpWeChatOfficialOptionsProvider> logger)
    {
        _logger = logger;
    }

    public virtual Task<AbpWeChatOfficialOptions> GetOrNullAsync(string appId)
    {
        _logger.LogWarning("Please implement IAbpWeChatOfficialOptionsProvider");

        return Task.FromResult<AbpWeChatOfficialOptions>(null);
    }
}