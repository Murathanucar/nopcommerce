using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Client.ReferanceCode.Data.Domain;

namespace Nop.Plugin.Client.ReferanceCode.Data
{
    [NopMigration("2024-02-20 10:10:10", "Referance Code Plugin Installiation Migrate", MigrationProcessType.Installation)]
    public class SchemeMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<RefCode>();
        }
    }
}
