using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Client.ReferanceCode.Models;
using Nop.Plugin.Client.ReferanceCode.Services;
using Nop.Services.Customers;
using Nop.Web.Controllers;

namespace Nop.Plugin.Client.ReferanceCode.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ReferanceCodeController : BasePublicController
    {
        #region Fields

        private readonly IWorkContext _workContext;
        private readonly ClientReferanceCodeService _clientReferanceCodeService;

        #endregion

        #region Ctor

        public ReferanceCodeController(IWorkContext workContext, ClientReferanceCodeService clientReferanceCodeService)
        {
            _workContext = workContext;
            _clientReferanceCodeService = clientReferanceCodeService;
        }

        #endregion


        public async Task<IActionResult>Show()
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var model = new CustomerReferanceCodeShowModel();
            var refTableRows = await _clientReferanceCodeService.GetAllCodesAsync(customerId: customer.Id);
            foreach (var refTableRow in refTableRows)
            {
                var codeModel = new CustomerReferanceCodeShowModel.CustomerReferanceCodeDetailModel
                {
                    Code = refTableRow.Code,
                    NumberOfUsed = refTableRow.NumberOfUsed
                };
                model.ReferanceCodes.Add(codeModel);
            }
            return View("~/Plugins/Client.ReferanceCode/Views/CustomerReferanceCode.cshtml", model);
        }

    }
}
