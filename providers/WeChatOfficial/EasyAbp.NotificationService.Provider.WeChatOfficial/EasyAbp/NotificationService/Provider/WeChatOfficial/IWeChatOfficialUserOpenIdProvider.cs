using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public interface IWeChatOfficialUserOpenIdProvider
{
    Task<string> GetOrNullAsync([NotNull] string appId, Guid userId);

    Task<string> GetOrNullAsync(Guid userId);
}