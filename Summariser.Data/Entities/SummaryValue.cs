using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Summariser.Data.Entities
{
	public class SummaryValue
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Value { get; set; }
		public DateTime LastModified { get; set; }
	}
}
