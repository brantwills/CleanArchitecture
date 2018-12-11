using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator(IDistributedCache redis)
        {
            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => redis.GetString($"customer:{v.Id}")).NotNull().WithMessage("Customer must exist to update");
        }
    }
}
