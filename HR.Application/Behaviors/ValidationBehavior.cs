using FluentValidation;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Behaviors
{
    public class ValidationBehavior <TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if(failures.Count != 0)
                {
                    var errors = failures.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();

                    if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(ApiResponse<>))
                    {
                        var resultType = typeof(TResponse).GetGenericArguments()[0];
                        var failureMethod = typeof(ApiResponse<>)
                            .MakeGenericType(resultType)
                            .GetMethod(nameof(ApiResponse<object>.FailureResponse));

                        return (TResponse)failureMethod!.Invoke(null, new object[] { errors, "Validation failed" })!;
                    }
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
