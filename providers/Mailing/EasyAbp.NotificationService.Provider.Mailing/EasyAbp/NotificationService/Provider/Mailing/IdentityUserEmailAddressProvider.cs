using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    [Dependency(TryRegister = true)]
    public class IdentityUserEmailAddressProvider : IUserEmailAddressProvider, ITransientDependency
    {
        private readonly IExternalUserLookupServiceProvider _userLookupServiceProvider;

        public IdentityUserEmailAddressProvider(
            IExternalUserLookupServiceProvider userLookupServiceProvider)
        {
            _userLookupServiceProvider = userLookupServiceProvider;
        }
        
        public virtual async Task<string> GetAsync(Guid userId)
        {
            var userData = await _userLookupServiceProvider.FindByIdAsync(userId);

            return userData?.Email;
        }
    }
}