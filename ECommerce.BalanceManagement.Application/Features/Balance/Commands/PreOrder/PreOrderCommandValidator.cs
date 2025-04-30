using FluentValidation;

namespace ECommerce.BalanceManagement.Application.Features.Balance.Commands.PreOrder;

public class PreOrderCommandValidator : AbstractValidator<PreOrderCommand>
{
    public PreOrderCommandValidator()
    {
        RuleFor(x => x.UserToken)
            .NotEmpty()
            .WithMessage("User token is required.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product id is required.");

        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Amount is required.")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");
    }
}