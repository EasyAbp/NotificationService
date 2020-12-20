using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.NotificationService.NotificationInfos
{
    public static class NotificationInfoEfCoreQueryableExtensions
    {
        public static IQueryable<NotificationInfo> IncludeDetails(this IQueryable<NotificationInfo> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) // TODO: AbpHelper generated
                ;
        }
    }
}