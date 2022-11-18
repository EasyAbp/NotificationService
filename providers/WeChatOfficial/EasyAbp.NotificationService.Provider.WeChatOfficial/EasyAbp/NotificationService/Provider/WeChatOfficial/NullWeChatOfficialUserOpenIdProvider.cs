using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[Dependency(TryRegister = true)]
public class NullWeChatOfficialUserOpenIdProvider : IWeChatOfficialUserOpenIdProvider, ITransientDependency
{
    private readonly ILogger<NullWeChatOfficialUserOpenIdProvider> _logger;

    public NullWeChatOfficialUserOpenIdProvider(ILogger<NullWeChatOfficialUserOpenIdProvider> logger)
    {
        _logger = logger;
    }

    public virtual Task<string> GetOrNullAsync(string appId, Guid userId)
    {
        _logger.LogWarning("Please implement IWeChatOfficialUserOpenIdProvider");

        return Task.FromResult<string>(null);
    }

    public virtual Task<string> GetOrNullAsync(Guid userId)
    {
        _logger.LogWarning("Please implement IWeChatOfficialUserOpenIdProvider");

        return Task.FromResult<string>(null);
    }
}