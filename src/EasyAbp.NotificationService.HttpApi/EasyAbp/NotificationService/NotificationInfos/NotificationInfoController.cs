using System;
using EasyAbp.NotificationService.NotificationInfos.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.NotificationService.NotificationInfos
{
    [RemoteService(Name = NotificationServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/notification-service/notification-info")]
    public class NotificationInfoController : NotificationServiceController, INotificationInfoAppService
    {
        private readonly INotificationInfoAppService _service;

        public NotificationInfoController(INotificationInfoAppService service)
        {
            _service = service;
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