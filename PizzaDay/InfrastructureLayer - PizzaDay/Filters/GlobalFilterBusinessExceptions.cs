using AplicationDomainLayer___PizzaDay.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace InfrastructureLayer___PizzaDay.Filters
{
    public class GlobalFilterBusinessExceptions : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception.GetType() == typeof(GlobalBusinessExceptions))
            {
                var exceptionMessage = (GlobalBusinessExceptions)context.Exception;

                var MessageToShow = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    message = exceptionMessage.Message 
                };

                var json = new
                {
                    errors = new[] { MessageToShow }
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
