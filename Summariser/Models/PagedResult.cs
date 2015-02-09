using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Summariser.Models
{
    [DataContract]
    public class PagedResult<TOut>
    {
        [DataMember]
        public List<LinkModel> Links { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public int TotalPages { get; set; }
        [DataMember]
        public IEnumerable<TOut> Results { get; set; }
    }
}