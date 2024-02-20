using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Customer;

namespace Nop.Plugin.Client.ReferanceCode
{
    public class CustomEvent : BasePlugin, IConsumer<ModelPreparedEvent<BaseNopModel>>
    {

        private readonly LocalizationService _localizationService;


        public CustomEvent(LocalizationService localizationService)
        {
                _localizationService = localizationService;
        }


        public async Task HandleEventAsync(ModelPreparedEvent<BaseNopModel> eventMessage)
        {
            if (eventMessage?.Model is CustomerNavigationModel customerNavigationModel)
            {
                var orderItem = customerNavigationModel.CustomerNavigationItems.FirstOrDefault(item => item.Tab == (int)CustomerNavigationEnum.Orders);
                var position = customerNavigationModel.CustomerNavigationItems.IndexOf(orderItem) + 1;
                customerNavigationModel.CustomerNavigationItems.Insert(position,new CustomerNavigationItemModel
                {
                    RouteName = ReferanceCodeDefaults.ReferanceCodeRouteName,
                    Title = await _localizationService.GetResourceAsync("Plugins.Client.ReferanceCode.Code"),
                    Tab = ReferanceCodeDefaults.ReferanceCodeMenuTab,
                    ItemClass = ReferanceCodeDefaults.ReferanceCodeClassName
                });
            }
        }
    }
}