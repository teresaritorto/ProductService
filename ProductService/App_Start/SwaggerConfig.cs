using System.Web.Http;
using Swashbuckle.Application;
using ProductService.Security;

namespace ProductService
{
    /// <summary>
    /// Swagger configuration
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Register config
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config
                .EnableSwagger(c =>
                {                   
                    c.SingleApiVersion("v1", "ProductService");
                    c.PrettyPrint();
                    c.OAuth2("oauth2")
                        .Description("OAuth2 Grant")
                        .Flow("password")
                        .TokenUrl("/api/getsecuretoken")
                        .Scopes(scopes =>
                        {
                            scopes.Add("swagger", "swagger");
                        });

                    c.GroupActionsBy(apiDesc => apiDesc.HttpMethod.ToString());                 
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    c.OperationFilter<AssignOAuth2SecurityRequirements>();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Product Service Test Page - Swagger");
                    c.EnableOAuth2Support("swagger", "doco", "Swagger UI");
                });
        }

        /// <summary>
        /// Get assembly xml comments path
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\ProductService.xml",
                System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
