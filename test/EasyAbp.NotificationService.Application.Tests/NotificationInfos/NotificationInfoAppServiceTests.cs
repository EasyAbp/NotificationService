using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.NotificationService.NotificationInfos
{
    public class NotificationInfoAppServiceTests : NotificationServiceApplicationTestBase
    {
        private readonly INotificationInfoAppService _notificationInfoAppService;

        public NotificationInfoAppServiceTests()
        {
            _notificationInfoAppService = GetRequiredService<INotificationInfoAppService>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
        */
    }
}
