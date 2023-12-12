﻿using FluentValidation;
using MediatR;


namespace Ordering.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var content = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(x => x.ValidateAsync(content, cancellationToken)));

            var failures = validationResults.SelectMany(x => x.Errors).Where(x => x != null).ToList();
            if (failures.Count != 0)
            {
                throw new Exceptions.ValidationException(failures);
            }
        }

        return await next();
    }
}
