using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProductService.App_Start
{
    /// <summary>
    /// Swagger Auth Filter
    /// </summary>
    public class AssignOAuth2SecurityRequirements : IOperationFilter
    {
        /// <summary>
        /// Apply Oauth requriements for swagger
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var actionFilters = apiDescription.ActionDescriptor.GetFilterPipeline();
            var allowsAnonymous = actionFilters.Select(f => f.Instance).OfType<OverrideAuthorizationAttribute>().Any();
            if (allowsAnonymous)
                return; // must be an anonymous method
            
            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>{ {"oauth2", new List<string> { "swagger" } }};

            if (operation.security == null)
                operation.security = new List<IDictionary<string, IEnumerable<string>>>();

            operation.security.Add(oAuthRequirements);
        }
    }
}