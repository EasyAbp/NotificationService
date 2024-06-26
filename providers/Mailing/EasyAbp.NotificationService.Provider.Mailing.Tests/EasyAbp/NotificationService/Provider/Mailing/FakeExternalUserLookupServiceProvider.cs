﻿using System;
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
            if (id == NotificationServiceTestConsts.FakeUser1Id)
            {
                return new UserData(
                    NotificationServiceTestConsts.FakeUser1Id,
                    "fake1",
                    "user1@easyabp.io",
                    emailConfirmed: true);
            }
            
            if (id == NotificationServiceTestConsts.FakeUser2Id)
            {
                return new UserData(
                    NotificationServiceTestConsts.FakeUser2Id,
                    "fake2",
                    "user2@easyabp.io",
                    emailConfirmed: false);
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