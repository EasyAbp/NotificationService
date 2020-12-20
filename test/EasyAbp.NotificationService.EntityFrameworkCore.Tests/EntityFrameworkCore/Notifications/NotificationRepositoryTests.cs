using System;
using System.Threading.Tasks;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.NotificationService.EntityFrameworkCore.Notifications
{
    public class NotificationRepositoryTests : NotificationServiceEntityFrameworkCoreTestBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationRepositoryTests()
        {
            _notificationRepository = GetRequiredService<INotificationRepository>();
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
