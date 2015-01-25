using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Summariser.Data;
using Summariser.Data.Entities;
using Summariser.Models;

namespace Summariser.Controllers
{
	[RoutePrefix("api/values")]
	public class ValuesController : ApiController
	{
		private const int DefaultPageSize = 10;
		private readonly ISummariserRepository _repository;
		private readonly ModelFactory _modelFactory;


		public ValuesController(ISummariserRepository repository)
		{
			_repository = repository;
			_modelFactory =  new ModelFactory();
		}


		[Route("", Name = "values")]
		public object GetAllValues(int page = 0, int pageSize = DefaultPageSize)
		{
			if (page < 0)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "page must not be negative");
			}

			var allValues = _repository.GetAllValues();
			Func<SummaryValue, SummaryValueModel> selector = s => _modelFactory.Create(s);
			var orderedValues = allValues.OrderBy(v => v.Id);

			var defaultResultPager = new DefaultResultPager(Request, "values");
			object pagedResults = defaultResultPager.GetPagedResults(page, pageSize, orderedValues, selector);
			return pagedResults;
		}

		public HttpResponseMessage Get(int id)
		{
			var summaryValue = _repository.GetValue(id);
			if (summaryValue == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			return Request.CreateResponse(HttpStatusCode.OK, _modelFactory.Create(summaryValue));
		}

		public HttpResponseMessage Post([FromBody] SummaryValueModel valueToInsert)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest);
				}

				// should not exist yet
				var existingValue = _repository.GetValue(valueToInsert.Id);
				if (existingValue != null)
				{
					return Request.CreateResponse(HttpStatusCode.Conflict, "A value with this id already exists.");
				}

				var entity = _modelFactory.Parse(valueToInsert);
				var insertedAndSaved = _repository.Insert(entity) && _repository.SaveAll();

				if (insertedAndSaved)
				{
					return Request.CreateResponse(HttpStatusCode.Created, _modelFactory.Create(entity));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return Request.CreateResponse(HttpStatusCode.BadRequest);
		}


		[HttpPut]
		[HttpPatch]
		public HttpResponseMessage Put(int id, [FromBody] SummaryValueModel valueToModify)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid value provided.");
				}

				var existingEntity = _repository.GetValue(id);
				if (existingEntity == null)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound);
				}

				var parsedEntity = _modelFactory.Parse(valueToModify);
				existingEntity.Value = parsedEntity.Value;
				existingEntity.LastModified = DateTime.UtcNow;

				if (_repository.SaveAll())
				{
					return Request.CreateResponse(HttpStatusCode.OK);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			return Request.CreateResponse(HttpStatusCode.BadRequest);
		}

		public HttpResponseMessage Delete(int id)
		{
			try
			{
				var existingEntity = _repository.GetValue(id);
				if (existingEntity == null)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound);
				}
				bool deletedAndSaved = _repository.Delete(id) && _repository.SaveAll();
				if (deletedAndSaved)
				{
					return Request.CreateResponse(HttpStatusCode.OK);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			return Request.CreateResponse(HttpStatusCode.BadRequest);
		}
	}
}