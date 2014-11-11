using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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


		public IEnumerable<SummaryValueModel> Get()
		{
			var summaryValues = _repository.GetAllValues().ToList().Select(s => _modelFactory.Create(s));
			return summaryValues;
		}

		public HttpResponseMessage Get(Guid id)
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
		public HttpResponseMessage Put(Guid id, [FromBody] SummaryValueModel valueToModify)
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

		public HttpResponseMessage Delete(Guid id)
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