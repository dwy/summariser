﻿using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using CacheCow.Server;
using CacheCow.Server.EntityTagStore.Memcached;
using CacheCow.Server.EntityTagStore.SqlServer;
using Enyim.Caching.Configuration;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Formatting.Jsonp;

namespace Summariser
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

		    var xmlFormatter = config.Formatters.OfType<XmlMediaTypeFormatter>().FirstOrDefault();

			var formatter = new JsonpMediaTypeFormatter(jsonFormatter, "callback");
			config.Formatters.Add(formatter);

			var memcachedEntityTagStore = new MemcachedEntityTagStore(new MemcachedClientConfiguration());
			var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			var sqlEntityTagStore = new SqlServerEntityTagStore(connectionString);
			var cacheHandler = new CachingHandler(config, sqlEntityTagStore);
			config.MessageHandlers.Add(cacheHandler);
		}
	}
}
