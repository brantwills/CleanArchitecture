using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator  : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator(IContext redis)
        {
            RuleFor(v => v.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(v => v.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("ID must be greater than zero");
            RuleFor(v => redis.Cache.GetHashed<Customer>("customer:hash", $"customer:id:{v.Id}")).Null().WithMessage("Customer already exists");
        }
    }
}
