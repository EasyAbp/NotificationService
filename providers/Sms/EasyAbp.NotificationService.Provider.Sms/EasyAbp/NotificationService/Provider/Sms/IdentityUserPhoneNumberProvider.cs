using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Sms
{
    [Dependency(TryRegister = true)]
    public class IdentityUserPhoneNumberProvider : IUserPhoneNumberProvider, ITransientDependency
    {
        private readonly IExternalUserLookupServiceProvider _userLookupServiceProvider;

        public IdentityUserPhoneNumberProvider(
            IExternalUserLookupServiceProvider userLookupServiceProvider)
        {
            _userLookupServiceProvider = userLookupServiceProvider;
        }

        public virtual async Task<string> GetAsync(Guid userId)
        {
            var userData = await _userLookupServiceProvider.FindByIdAsync(userId);

            if (userData is null || !userData.PhoneNumberConfirmed || userData.PhoneNumber.IsNullOrWhiteSpace())
            {
                return null;
            }

            return userData.PhoneNumber;
        }
    }
}