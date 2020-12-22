using System;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public interface IUserEmailAddressProvider
    {
        Task<string> GetAsync(Guid userId);
    }
}