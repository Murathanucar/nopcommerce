using Microsoft.Extensions.Logging;
using Nop.Data;
using Nop.Plugin.Client.ReferanceCode.Data.Domain;

namespace Nop.Plugin.Client.ReferanceCode.Services
{
    public class ClientReferanceCodeService
    {
        private readonly IRepository<RefCode> _refCodeRepository;

        public ClientReferanceCodeService(IRepository<RefCode> refCodeRepository)
        {
            _refCodeRepository = refCodeRepository;
        }

        /// <summary>
        /// Insert the RefCode record
        /// </summary>
        /// <param name="refCode">RefCode record</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task InsertAsync(RefCode refCode)
        {
            await _refCodeRepository.InsertAsync(refCode, false);
        }

        public async Task<IList<RefCode>> GetAllCodesAsync(int customerId = 0)
        {
            return await _refCodeRepository.GetAllAsync(query =>
            {
                if (customerId > 0)
                    query = query.Where(code => code.CustomerId == customerId);
                return query;
            }, null);
        }

        public async Task<IList<RefCode>> GetCustomersAsync(string refCode)
        {
            return await _refCodeRepository.GetAllAsync(query =>
            {
                if (refCode is not null)
                {
                    query = query.Where(rc => rc.Code == refCode);
                    return query;
                }
                else
                {
                    return query;
                }
            }, null);
        }

        public RefCode? GetCustomer(string refCode)
        {
            return _refCodeRepository.Table.FirstOrDefault(x => x.Code == refCode);
        }

    }


}