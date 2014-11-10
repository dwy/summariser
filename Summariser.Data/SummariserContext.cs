using System.Data.Entity;
using Summariser.Data.Entities;

namespace Summariser.Data
{
	public class SummariserContext : DbContext
	{
		public SummariserContext()
			: base("DefaultConnection")
		{
			
		}

		public DbSet<SummaryValue> SummaryValues { get; set; }
	}
}
