using System.Linq;
using Summariser.Data.Entities;

namespace Summariser.Data
{
	public interface ISummariserRepository
	{
		IQueryable<SummaryValue> GetAllValues();
		SummaryValue GetValue(int id);

		bool SaveAll();
		bool Insert(SummaryValue value);
		bool Delete(int id);
	}
}