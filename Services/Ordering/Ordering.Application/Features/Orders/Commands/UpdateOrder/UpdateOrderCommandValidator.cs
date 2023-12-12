using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("UserName is required")
            .NotEmpty().WithMessage("UserName can not be empty")
            .MinimumLength(3).WithMessage("UserName must be longer");

        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Email is required");

        RuleFor(x => x.TotalPrice)
            .NotEmpty().WithMessage("TotalPrice is required")
            .GreaterThan(0).WithMessage("TotalPrice must be greater than zero");
    }
}
