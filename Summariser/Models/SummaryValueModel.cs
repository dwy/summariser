using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Summariser.Models
{
	public class SummaryValueModel
	{
		public int Id { get; set; }

		public string Value { get; set; }
		public DateTime LastModified { get; set; }
	}
}