using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;
using Summariser.Data.Entities;
using Summariser.Models;

namespace Summariser.Controllers
{
	public class DefaultResultPager
	{
		private readonly HttpRequestMessage _request;
		private readonly string _routeName;

		public DefaultResultPager(HttpRequestMessage request, string routeName)
		{
			_request = request;
			_routeName = routeName;
		}

		public object GetPagedResults(int page, int pageSize, IEnumerable<SummaryValue> allResults, 
			Func<SummaryValue, SummaryValueModel> createModel)
		{
			var modelFactory = new ModelFactory();
			var links = new List<LinkModel>();
			var urlHelper = new UrlHelper(_request);

			var totalCount = allResults.Count();
			var totalPages = Math.Ceiling((double) totalCount/pageSize);

			if (page > 0)
			{
				string prevPageUrl = urlHelper.Link(_routeName, new {page = page - 1});
				links.Add(modelFactory.CreateLink(prevPageUrl, "prevPage"));
			}

			if (page < totalPages - 1)
			{
				string nextPageUrl = urlHelper.Link(_routeName, new {page = page + 1});
				links.Add(modelFactory.CreateLink(nextPageUrl, "nextPage"));
			}

			var pagedValues = allResults.Skip(pageSize * page).Take(pageSize).ToList()
				.Select(createModel); 

			return new
			{
				links,
				totalCount,
				totalPages,
				results = pagedValues
			};
		}
	}
}