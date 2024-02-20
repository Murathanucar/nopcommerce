using Nop.Web.Framework.Models;

namespace Nop.Plugin.Client.ReferanceCode.Models
{
    /// <summary>
    /// Represents referance code model
    /// </summary>
    public record CustomerReferanceCodeShowModel : BaseNopModel
    {
        #region Ctor

        public CustomerReferanceCodeShowModel()
        {
            ReferanceCodes = new List<CustomerReferanceCodeDetailModel>();
        }

        #endregion

        #region Properties

        public IList<CustomerReferanceCodeDetailModel> ReferanceCodes { get; set; }

        #endregion

        #region Nested classes

        public record CustomerReferanceCodeDetailModel : BaseNopEntityModel
        {
            public string? Code { get; set; }

            public int NumberOfUsed { get; set; }
        }

        #endregion
    }
}