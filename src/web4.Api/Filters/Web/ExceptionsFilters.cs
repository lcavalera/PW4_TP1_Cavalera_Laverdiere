using Events.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Events.Api.Filters.Web
{
    public class ExceptionsFilters : IActionFilter
    {
        private readonly ILogger<ExceptionsFilters> _logger;

        public ExceptionsFilters(ILogger<ExceptionsFilters> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is HttpException exception)
                {
                    //object que existe deja
                    context.Result = new ObjectResult(exception.Errors)
                    {
                        StatusCode = exception.StatusCode
                    };
                    _logger.LogError(context.Exception, "failed to handle request: {mdg}", exception.Errors); //pour garder la trace du log
                }
                else
                {
                    //object anonyme
                    context.Result = new ObjectResult(new ProblemDetails { Title = "Internal server error", Status = 500, Detail = context.Exception.Message })
                    {
                        StatusCode = 500
                    };

                    _logger.LogError(context.Exception, "internal server error {mdg}", context.Exception.Message); //pour garder la trace du log

                }
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                _logger.LogWarning("model state invalid");
            }
        }
    }
}
