using System;
using System.Linq;
using Summariser.Data.Entities;

namespace Summariser.Data
{
	public interface ISummariserRepository
	{
		IQueryable<SummaryValue> GetAllValues();
		SummaryValue GetValue(Guid id);

		bool SaveAll();
	}
}