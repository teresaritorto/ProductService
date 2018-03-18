using NLog;
using System.Web.Http.ExceptionHandling;

namespace ProductService.Logging
{
    /// <summary>
    /// Log unhandled exceptions
    /// </summary>
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="context"></param>
        public override void Log(ExceptionLoggerContext context)
        {
            var messageMetadata = $"Method: [{context.Request.Method}] - RequestUri: [{context.Request.RequestUri}]";
            Nlog.Log(LogLevel.Error, messageMetadata, context.Exception);
        }
    }
}