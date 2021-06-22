using System;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public interface IUserUserNameProvider
    {
        Task<string> GetAsync(Guid userId);
    }
}