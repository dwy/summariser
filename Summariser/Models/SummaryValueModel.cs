using System;
using System.Runtime.Serialization;

namespace Summariser.Models
{
    [DataContract]
	public class SummaryValueModel
	{
        [DataMember]
		public int Id { get; set; }

        [DataMember]
		public string Value { get; set; }

        [DataMember]
		public DateTime LastModified { get; set; }
	}
}