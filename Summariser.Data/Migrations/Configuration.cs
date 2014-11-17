using Summariser.Data.Entities;

namespace Summariser.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Summariser.Data.SummariserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
	        AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SummariserContext context)
        {
	        for (int i = 1; i <= 100; i++)
	        {
		        context.SummaryValues.AddOrUpdate(s => s.Id, 
					new SummaryValue
					{
						Value = "value " + i,
						LastModified = DateTime.UtcNow
					});
	        }
        }
    }
}
