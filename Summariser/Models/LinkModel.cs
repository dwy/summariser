using System.Runtime.Serialization;

namespace Summariser.Models
{
    [DataContract]
	public class LinkModel
	{
        [DataMember]
		public string Href { get; set; }
        [DataMember]
		public string Rel { get; set; }
        [DataMember]
		public string Method { get; set; }
	}
}