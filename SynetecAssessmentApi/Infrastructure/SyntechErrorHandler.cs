using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SynetecAssessmentApi.Dtos;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Infrastructure
{
    public class SyntechErrorHandler
    {
        #region Private Fields

        private readonly RequestDelegate _next;

        private static ILogger<SyntechErrorHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        ///     Standard middleware constructor
        /// </summary>
        public SyntechErrorHandler(RequestDelegate next, ILogger<SyntechErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Catches Web API exceptions thrown by the http pipe-line and generates
        ///     JSON response with 'error' property set to the exception's message. 
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Only process exception if it is an API. As far as I know, Path should never
                // be null and the Value should not be null. If it *is*, we don't crash on those nulls,
                // we just pass the original exception along.
                var path = context?.Request?.Path;
                if (path.HasValue && (path.Value.StartsWithSegments("/api", StringComparison.InvariantCulture)))
                {
                    await HandleExceptionAsync(context, ex).ConfigureAwait(false);
                }
                else
                {
                    _logger?.LogError(ex, $"Exception encountered while handling \"{path}\" request");
                    throw;
                }
            }
        }

        #endregion

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger?.LogError(exception, "Exception encountered while handling web API");
            // default to 500 if we don't expect the type of this exception
            var code = HttpStatusCode.InternalServerError;
            string errorCode = null;

            switch (exception)
            {
                case SyntechException applicationException:
                    return HandleSyntechException(context, applicationException);
            }

            var respone = new ErrorResponse { Error = exception.Message, ErrorCode = errorCode };

            var result = JsonSerializer.Serialize(respone);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleSyntechException(HttpContext context, SyntechException exception)
        {
            var exceptionType = exception.ExceptionType;
            var code = HttpStatusCode.InternalServerError;

            switch (exceptionType)
            {
                case SyntechExceptionType.InvalidData:
                    code = HttpStatusCode.BadRequest;
                    break;
                case SyntechExceptionType.Default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                error = exception.Message,
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
