using Summariser.Data.Entities;

namespace Summariser.Models
{
	public class ModelFactory
	{
		public SummaryValueModel Create(SummaryValue value)
		{
			return new SummaryValueModel
			{
				Id = value.Id,
				LastModified = value.LastModified,
				Value = value.Value
			};
		} 
	}
}