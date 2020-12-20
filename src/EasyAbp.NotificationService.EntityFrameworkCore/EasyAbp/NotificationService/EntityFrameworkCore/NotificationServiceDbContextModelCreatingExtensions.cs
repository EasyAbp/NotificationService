using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.NotificationService.EntityFrameworkCore
{
    public static class NotificationServiceDbContextModelCreatingExtensions
    {
        public static void ConfigureNotificationService(
            this ModelBuilder builder,
            Action<NotificationServiceModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new NotificationServiceModelBuilderConfigurationOptions(
                NotificationServiceDbProperties.DbTablePrefix,
                NotificationServiceDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */


            builder.Entity<Notification>(b =>
            {
                b.ToTable(options.TablePrefix + "Notifications", options.Schema);
                b.ConfigureByConvention(); 
                

                /* Configure more properties here */
            });


            builder.Entity<NotificationInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "NotificationInfos", options.Schema);
                b.ConfigureByConvention(); 
                

                /* Configure more properties here */
            });
        }
    }
}
