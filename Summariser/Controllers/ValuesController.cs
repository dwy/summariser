using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Summariser.Data;
using Summariser.Data.Entities;

namespace Summariser.Controllers
{
	public class ValuesController : ApiController
	{
		private readonly ISummariserRepository _repository;

		public ValuesController(ISummariserRepository repository)
		{
			_repository = repository;
		}


		// GET api/<controller>
		public IEnumerable<SummaryValue> Get()
		{
			var summaryValues = _repository.GetAllValues().ToList();
			return summaryValues;
		}

		// GET api/<controller>/5
		public SummaryValue Get(Guid id)
		{
			var summaryValue = _repository.GetValue(id);
			return summaryValue;
		}
	}
}