using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class FakeWeChatOfficialUserOpenIdProvider : IWeChatOfficialUserOpenIdProvider, ITransientDependency
{
    public virtual Task<string> GetOrNullAsync(string appId, Guid userId)
    {
        return GetOrNullAsync(userId);
    }

    public virtual Task<string> GetOrNullAsync(Guid userId)
    {
        if (userId == NotificationServiceProviderWeChatOfficialTestConsts.FakeUser1Id)
        {
            return Task.FromResult("my-openid-1");
        }

        if (userId == NotificationServiceProviderWeChatOfficialTestConsts.FakeUser2Id)
        {
            return Task.FromResult("my-openid-2");
        }

        return Task.FromResult<string>(null);
    }
}