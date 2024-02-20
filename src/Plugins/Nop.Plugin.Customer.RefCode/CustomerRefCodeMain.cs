using Microsoft.Extensions.Logging;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.ScheduleTasks;
using Nop.Core.Infrastructure;
using Nop.Plugin.Client.ReferanceCode.Data.Domain;
using Nop.Plugin.Client.ReferanceCode.Services;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.Client.ReferanceCode
{
    public class CustomerRefCodeMain : BasePlugin, IConsumer<CustomerRegisteredEvent>, IMiscPlugin
    {
        private readonly IWorkContext _workContext;
        private readonly ClientReferanceCodeService _refCodeService;
        private readonly LocalizationService _localizationService;
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly ILogger<CustomerRefCodeMain> _logger;

        public CustomerRefCodeMain(IWorkContext workContext,
            ClientReferanceCodeService refCodeService,
            LocalizationService localizationService,
            IScheduleTaskService scheduleTaskService,
            ILogger<CustomerRefCodeMain> logger
            )
        {
            _workContext = workContext;
            _refCodeService = refCodeService;
            _localizationService = localizationService;
            _scheduleTaskService = scheduleTaskService;
            _logger = logger;
        }

        public async Task HandleEventAsync(CustomerRegisteredEvent eventMessage)
        {
            var customer = eventMessage.Customer;
            await _refCodeService.InsertAsync(new RefCode
            {
                CustomerId = customer.Id,
                Code = "123456",
                CreatedOn = DateTime.Now,
                NumberOfUsed = 0
            }
                );
        }


        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Client.ReferanceCode.Code"] = "Referans Kodu",
            });

            _logger.LogInformation("Plugins.Client.ReferanceCode.Code Plugin install begin.");

            //install synchronization task
            if (await _scheduleTaskService.GetTaskByTypeAsync(ReferanceCodeDefaults.SynchronizationTask) == null)
            {
                await _scheduleTaskService.InsertTaskAsync(new ScheduleTask
                {
                    Enabled = true,
                    LastEnabledUtc = DateTime.UtcNow,
                    Seconds = ReferanceCodeDefaults.DefaultSynchronizationPeriod * 60 * 60,
                    Name = ReferanceCodeDefaults.SynchronizationTaskName,

                    Type = ReferanceCodeDefaults.SynchronizationTask,
                });
            }

            await base.InstallAsync();
        }


    }
}
