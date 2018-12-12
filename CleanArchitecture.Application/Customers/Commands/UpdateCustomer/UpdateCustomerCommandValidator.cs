using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator(IContext redis)
        {
            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => redis.Cache.GetHashed<Customer>("customer:hash", $"customer:id:{v.Id}")).NotNull().WithMessage("Customer must exist to update");
        }
    }
}
