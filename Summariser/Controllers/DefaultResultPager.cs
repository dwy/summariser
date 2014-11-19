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
		public DefaultResultPager()
		{
		}

		public object GetPagedResults(int page, int pageSize, IEnumerable<SummaryValue> allResults, 
			IEnumerable<SummaryValueModel> pagedResults, HttpRequestMessage request)
		{
			var modelFactory = new ModelFactory();
			var links = new List<LinkModel>();
			var urlHelper = new UrlHelper(request);

			var totalCount = allResults.Count();
			var totalPages = Math.Ceiling((double) totalCount/pageSize);

			if (page > 0)
			{
				string prevPageUrl = urlHelper.Link("values", new {page = page - 1});
				links.Add(modelFactory.CreateLink(prevPageUrl, "prevPage"));
			}

			if (page < totalPages - 1)
			{
				string nextPageUrl = urlHelper.Link("values", new {page = page + 1});
				links.Add(modelFactory.CreateLink(nextPageUrl, "nextPage"));
			}

			return new
			{
				links,
				totalCount,
				totalPages,
				results = pagedResults
			};
		}
	}
}