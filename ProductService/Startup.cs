using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(ProductService.Startup))]
namespace ProductService
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configure startup
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            //ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            SwaggerConfig.Register(config);            
        } 
    }
}