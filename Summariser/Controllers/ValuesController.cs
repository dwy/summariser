using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Summariser.Data;
using Summariser.Models;

namespace Summariser.Controllers
{
	public class ValuesController : ApiController
	{
		private readonly ISummariserRepository _repository;
		private readonly ModelFactory _modelFactory;

		public ValuesController(ISummariserRepository repository)
		{
			_repository = repository;
			_modelFactory =  new ModelFactory();
		}


		// GET api/<controller>
		public IEnumerable<SummaryValueModel> Get()
		{
			var summaryValues = _repository.GetAllValues().ToList().Select(s => _modelFactory.Create(s));
			return summaryValues;
		}

		// GET api/<controller>/5
		public SummaryValueModel Get(Guid id)
		{
			var summaryValue = _modelFactory.Create(_repository.GetValue(id));
			return summaryValue;
		}
	}
}