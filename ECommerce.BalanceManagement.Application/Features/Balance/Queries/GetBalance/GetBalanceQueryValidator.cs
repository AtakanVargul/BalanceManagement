using FluentValidation;

namespace ECommerce.BalanceManagement.Application.Features.Balance.Queries.GetBalance;

public class GetBalanceQueryValidator : AbstractValidator<GetBalanceQuery>
{
    public GetBalanceQueryValidator()
    {
        RuleFor(x => x.UserToken)
            .NotEmpty()
            .WithMessage("User token is required.");
    }
}