using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Client.ReferanceCode.Data.Domain;

namespace Nop.Plugin.Client.ReferanceCode.Data.Builders
{
    public class RefCodeBuilder : NopEntityBuilder<RefCode>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(RefCode.CustomerId)).AsInt32().ForeignKey<Customer>();
        }
    }
}
