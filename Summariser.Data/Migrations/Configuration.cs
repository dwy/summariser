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
        }
    }
}
