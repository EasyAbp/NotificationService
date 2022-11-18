using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public interface IAbpWeChatOfficialOptionsProvider
{
    [ItemCanBeNull]
    Task<AbpWeChatOfficialOptions> GetOrNullAsync([NotNull] string appId);
}