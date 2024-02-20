using Nop.Core;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Client.ReferanceCode.Extension;
using Nop.Services.Customers;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;

namespace Nop.Plugin.Client.ReferanceCode.Services
{
    /// <summary>
    /// Represents reward point manager
    /// </summary>
    public class RewardPointManager
    {
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly ILogger _logger;
        private readonly ClientReferanceCodeService _clientReferanceCodeService;
        private readonly RewardPointService _rewardPointService;
        private readonly CustomerService _customerService;

        public RewardPointManager(IOrderService orderService,
            IWorkContext workContext,
            ILogger logger,
            ClientReferanceCodeService clientReferanceCodeService,
            RewardPointService rewardPointService,
            CustomerService customerService)
        {
            _orderService = orderService;
            _workContext = workContext;
            _logger = logger;
            _clientReferanceCodeService = clientReferanceCodeService;
            _rewardPointService = rewardPointService;
            _customerService = customerService;
        }

        /// <summary>
        /// Tüm siparişleri kontrol ederek;
        /// -> Eğer sipariş başarılı ise
        /// </summary>
        /// <param name="synchronizationTask">Whether it's a scheduled synchronization</param>
        /// <param name="storeId">Store identifier; pass 0 to synchronize contacts for all stores</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of messages
        /// </returns>
        public async Task<IList<(NotifyType Type, string Message)>> SynchronizeAsync(bool synchronizationTask = true, int storeId = 0)
        {
            var messages = new List<(NotifyType, string)>();
            try
            {
                var orders = await _orderService.SearchOrdersAsync();
                foreach (var order in orders)
                {
                    if (!string.IsNullOrEmpty(order.CheckoutAttributesXml) &&
                        order.CreatedOnUtc.AddDays(14) < DateTime.UtcNow &&
                                order.PaymentStatus == PaymentStatus.Paid
                                )
                    {
                        var referenceCode = order.CheckoutAttributesXml.XmlToValue();

                        if (referenceCode is null)
                            continue;

                        var customerRefCode = _clientReferanceCodeService.GetCustomer(referenceCode);

                        if (customerRefCode is null)
                            continue;

                        var customerId = customerRefCode.CustomerId;
                        var customer = await _customerService.GetCustomerByIdAsync(customerId);
                        if (customerId != order.CustomerId)
                        {
                            var rewardPointsHistory = await _rewardPointService.AddRewardPointsHistoryEntryAsync(
                                customer: customer,
                                points: 100,
                                storeId: 1,
                                activatingDate: DateTime.UtcNow,
                                endDate: null
                            );
                        }

                    }
                }
            }
            catch (Exception exception)
            {
                //log full error
                await _logger.ErrorAsync($"Reward point synchronization error: {exception.Message}.", exception, await _workContext.GetCurrentCustomerAsync());
                messages.Add((NotifyType.Error, $"Reward point synchronization error: {exception.Message}"));
            }

            return messages;
        }
    }
}