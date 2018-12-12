using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator(IContext redis)
        {
            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => redis.Cache.GetHashed<Customer>("customer:hash", $"customer:id:{v.Id}")).NotNull().WithMessage("Customer must exist to delete");
        }
    }
}
