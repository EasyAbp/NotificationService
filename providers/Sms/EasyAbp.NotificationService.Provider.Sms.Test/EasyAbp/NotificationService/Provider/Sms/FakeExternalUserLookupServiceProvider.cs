using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.NotificationService.Provider.Sms;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Sms
{
    public class FakeExternalUserLookupServiceProvider : IExternalUserLookupServiceProvider, ITransientDependency
    {
        public async Task<IUserData> FindByIdAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            if (id == NotificationServiceProviderSmsTestConsts.FakeUser1Id)
            {
                return new UserData(
                    NotificationServiceProviderSmsTestConsts.FakeUser1Id,
                    "fake1",
                    "user1@easyabp.io",
                    phoneNumber: "123456");
            }
            
            if (id == NotificationServiceProviderSmsTestConsts.FakeUser2Id)
            {
                return new UserData(
                    NotificationServiceProviderSmsTestConsts.FakeUser2Id,
                    "fake2",
                    "user2@easyabp.io",
                    phoneNumber: "654321");
            }

            return null;
        }

        public Task<IUserData> FindByUserNameAsync(string userName, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<List<IUserData>> SearchAsync(string sorting = null, string filter = null, int maxResultCount = 2147483647, int skipCount = 0,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}