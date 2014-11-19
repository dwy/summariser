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

		public SummaryValue Parse(SummaryValueModel value)
		{
			return new SummaryValue
			{
				Value = value.Value, LastModified = value.LastModified
			};
		}

		public LinkModel CreateLink(string href, string rel, string method = "GET")
		{
			return new LinkModel
			{
				Href = href,
				Rel = rel,
				Method = method
			};
		}
	}
}