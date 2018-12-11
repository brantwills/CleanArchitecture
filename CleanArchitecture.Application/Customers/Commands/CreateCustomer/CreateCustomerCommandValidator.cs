using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator  : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator(IDistributedCache redis)
        {
            RuleFor(v => v.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(v => v.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("ID must be greater than zero");
            RuleFor(v => redis.GetString($"customer:{v.Id}")).Null().WithMessage("Customer already exists");
        }
    }
}
