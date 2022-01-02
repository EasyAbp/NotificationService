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

        [HttpPost]
        public virtual Task<NotificationDto> CreateAsync(CreateUpdateNotificationDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<NotificationDto> UpdateAsync(Guid id, CreateUpdateNotificationDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<NotificationDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<NotificationDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}