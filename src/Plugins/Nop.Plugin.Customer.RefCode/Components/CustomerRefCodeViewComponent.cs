using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Client.ReferanceCode.Components
{
    public class CustomerRefCodeViewComponent : NopViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone)
        {
            return await Task.FromResult(View("~Plugins/Customer.RefCode/Views/CustomerRefCode.cshtml"));
        }
    }
}
