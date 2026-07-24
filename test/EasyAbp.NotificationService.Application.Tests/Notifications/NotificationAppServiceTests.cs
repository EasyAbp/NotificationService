using System;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;
using EasyAbp.NotificationService.Notifications.Dtos;

namespace EasyAbp.NotificationService.Notifications
{
    public class NotificationAppServiceTests : NotificationServiceApplicationTestBase
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IObjectMapper _objectMapper;

        public NotificationAppServiceTests()
        {
            _notificationAppService = GetRequiredService<INotificationAppService>();
            _objectMapper = GetRequiredService<IObjectMapper>();
        }

        [Fact]
        public void Should_Map_Notification_To_NotificationDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var notificationInfoId = Guid.NewGuid();
            var notification = new Notification(
                id,
                tenantId: null,
                userId: userId,
                userName: "test-user",
                notificationInfoId: notificationInfoId,
                notificationMethod: "Email");

            // Act
            var dto = _objectMapper.Map<Notification, NotificationDto>(notification);

            // Assert
            dto.Id.ShouldBe(id);
            dto.UserId.ShouldBe(userId);
            dto.UserName.ShouldBe("test-user");
            dto.NotificationInfoId.ShouldBe(notificationInfoId);
            dto.NotificationMethod.ShouldBe("Email");
        }
    }
}
