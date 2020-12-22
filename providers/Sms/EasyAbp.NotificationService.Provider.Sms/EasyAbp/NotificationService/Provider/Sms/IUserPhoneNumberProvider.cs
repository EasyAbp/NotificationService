using System;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Provider.Sms
{
    public interface IUserPhoneNumberProvider
    {
        Task<string> GetAsync(Guid userId);
    }
}