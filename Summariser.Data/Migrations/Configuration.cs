using Summariser.Data.Entities;

namespace Summariser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<SummariserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SummariserContext context)
        {
	        for (int i = 0; i < 50; i++)
	        {
				context.SummaryValues.AddOrUpdate(
					s => s.Id,
					new SummaryValue { Value = "value " + i, LastModified = DateTime.UtcNow }
				);
	        }

            // NOTE since CREATE/ALTER PROCEDURE needs to be the only statement in a batch, we create dummy procedures using
            // exec('CREATE PROCEDURE ...) and then execute ALTER for each separately.
#if RESET_DB_CACHE
            context.Database.ExecuteSqlCommand(Resources.CacheCow_CreateDatabaseAndProcedures);

            context.Database.ExecuteSqlCommand(Resources.AlterProcedure_AddOrUpdateCache);
            context.Database.ExecuteSqlCommand(Resources.AlterProcedure_ClearCache);
            context.Database.ExecuteSqlCommand(Resources.AlterProcedure_DeleteCacheById);
            context.Database.ExecuteSqlCommand(Resources.AlterProcedure_DeleteCacheByResourceUri);
            context.Database.ExecuteSqlCommand(Resources.AlterProcedure_DeleteCacheByRoutePattern);
            context.Database.ExecuteSqlCommand(Resources.AlterProcedure_GetCache);
#endif
        }
    }
}
