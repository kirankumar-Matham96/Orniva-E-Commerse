using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrnivaApi.Responses;

namespace OrnivaApi.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null)
                    continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = _serviceProvider.GetService(validatorType);

                if (validator is null)
                    continue;

                var validationContext = new ValidationContext<object>(argument);

                var validationResult = await ((IValidator)validator)
                    .ValidateAsync(validationContext);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            group => group.Key,
                            group => group
                                .Select(e => e.ErrorMessage)
                                .ToArray());

                    context.Result = new BadRequestObjectResult(
                        new ValidationResponse()
                        {
                            Success = false,
                            Message = "Validation Failed",
                            Errors = errors
                        }
                    );

                    return;
                }
            }

            await next();
        }
    }
}