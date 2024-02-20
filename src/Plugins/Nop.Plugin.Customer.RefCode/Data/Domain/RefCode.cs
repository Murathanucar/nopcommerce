using Nop.Core;

namespace Nop.Plugin.Client.ReferanceCode.Data.Domain
{
    public class RefCode : BaseEntity
    {
        public string Code { get; set; }

        public int CustomerId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int NumberOfUsed { get; set; } = 0;

    }
}
