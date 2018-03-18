﻿using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ProductService
{
    /// <summary>
    /// Web Api Config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register config
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
             name: "swagger_root",
             routeTemplate: "",
             defaults: null,
             constraints: null,
             handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger"));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //JSON Camel Case formatter
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }     
    }
}
