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
        }

        protected override void Seed(SummariserContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
	        for (int i = 0; i < 50; i++)
	        {
				context.SummaryValues.AddOrUpdate(
					s => s.Id,
					new SummaryValue { Value = "value " + i, LastModified = DateTime.Now }
				);
	        }
        }
    }
}
