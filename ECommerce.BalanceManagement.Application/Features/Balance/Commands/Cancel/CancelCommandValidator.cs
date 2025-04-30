using FluentValidation;

namespace ECommerce.BalanceManagement.Application.Features.Balance.Commands.Cancel;

public class CancelCommandValidator : AbstractValidator<CancelCommand>
{
    public CancelCommandValidator()
    {
        RuleFor(x => x.UserToken)
            .NotEmpty()
            .WithMessage("User token is required.");

        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order id is required.");
    }
}