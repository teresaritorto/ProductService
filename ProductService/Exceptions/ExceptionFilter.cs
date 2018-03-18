using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ProductService.Exceptions
{
    /// <summary>
    /// Exception Filter
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Handle Exception with generic message
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {            
            const string error = "An unexpected error has occured";
            context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error);
        }
    }
}