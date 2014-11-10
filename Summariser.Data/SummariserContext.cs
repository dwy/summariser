using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
