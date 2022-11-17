using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class FakeExternalUserLookupServiceProvider : IExternalUserLookupServiceProvider, ITransientDependency
    {
        public async Task<IUserData> FindByIdAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            if (id == NotificationServiceProviderMailingTestConsts.FakeUser1Id)
            {
                return new UserData(
                    NotificationServiceProviderMailingTestConsts.FakeUser1Id,
                    "fake1",
                    "user1@easyabp.io");
            }
            
            if (id == NotificationServiceProviderMailingTestConsts.FakeUser2Id)
            {
                return new UserData(
                    NotificationServiceProviderMailingTestConsts.FakeUser2Id,
                    "fake2",
                    "user2@easyabp.io");
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