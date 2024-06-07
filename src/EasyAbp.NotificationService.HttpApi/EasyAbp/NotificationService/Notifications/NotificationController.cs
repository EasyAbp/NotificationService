using System;
using EasyAbp.NotificationService.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.NotificationService.Notifications
{
    [RemoteService(Name = NotificationServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/notification-service/notification")]
    public class NotificationController : NotificationServiceController, INotificationAppService
    {
        private readonly INotificationAppService _service;

        public NotificationController(INotificationAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<NotificationDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<NotificationDto>> GetListAsync(NotificationGetListInput input)
        {
            return _service.GetListAsync(input);
        }
    }
}