using System;
using EasyAbp.NotificationService.NotificationInfos.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.NotificationService.NotificationInfos
{
    [RemoteService(Name = "EasyAbpNotificationService")]
    [Route("/api/notification-service/notification-info")]
    public class NotificationInfoController : NotificationServiceController, INotificationInfoAppService
    {
        private readonly INotificationInfoAppService _service;

        public NotificationInfoController(INotificationInfoAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual Task<NotificationInfoDto> CreateAsync(CreateUpdateNotificationInfoDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<NotificationInfoDto> UpdateAsync(Guid id, CreateUpdateNotificationInfoDto input)
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
        public virtual Task<NotificationInfoDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<NotificationInfoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}