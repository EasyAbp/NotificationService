using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Security;

public class TestExternalUserLookupServiceProvider : IExternalUserLookupServiceProvider, ITransientDependency
{
    public Task<IUserData> FindByIdAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
        var adminUserId = Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d");

        if (id == adminUserId)
        {
            return Task.FromResult<IUserData>(new UserData(adminUserId, "admin"));
        }

        if (id == NotificationServiceTestConsts.FakeUser1Id)
        {
            return Task.FromResult<IUserData>(new UserData(NotificationServiceTestConsts.FakeUser1Id, "user1"));
        }

        if (id == NotificationServiceTestConsts.FakeUser2Id)
        {
            return Task.FromResult<IUserData>(new UserData(NotificationServiceTestConsts.FakeUser2Id, "user2"));
        }

        if (id == NotificationServiceTestConsts.FakeUser3Id)
        {
            return Task.FromResult<IUserData>(new UserData(NotificationServiceTestConsts.FakeUser3Id, "user3"));
        }

        return Task.FromResult<IUserData>(new UserData(id, id.ToString()));
    }

    public Task<IUserData> FindByUserNameAsync(string userName,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotSupportedException();
    }

    public Task<List<IUserData>> SearchAsync(string sorting = null, string filter = null,
        int maxResultCount = 2147483647, int skipCount = 0,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotSupportedException();
    }

    public Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotSupportedException();
    }
}