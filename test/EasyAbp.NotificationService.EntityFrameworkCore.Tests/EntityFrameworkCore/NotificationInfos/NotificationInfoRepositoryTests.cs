using System;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.NotificationService.EntityFrameworkCore.NotificationInfos
{
    public class NotificationInfoRepositoryTests : NotificationServiceEntityFrameworkCoreTestBase
    {
        private readonly INotificationInfoRepository _notificationInfoRepository;

        public NotificationInfoRepositoryTests()
        {
            _notificationInfoRepository = GetRequiredService<INotificationInfoRepository>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
        */
    }
}
