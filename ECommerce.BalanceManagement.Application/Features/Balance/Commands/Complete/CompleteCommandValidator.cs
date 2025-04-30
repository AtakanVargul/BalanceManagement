using FluentValidation;

namespace ECommerce.BalanceManagement.Application.Features.Balance.Commands.Complete;

public class CompleteCommandValidator : AbstractValidator<CompleteCommand>
{
    public CompleteCommandValidator()
    {
        RuleFor(x => x.UserToken)
            .NotEmpty()
            .WithMessage("User token is required.");

        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order id is required.");
    }
}