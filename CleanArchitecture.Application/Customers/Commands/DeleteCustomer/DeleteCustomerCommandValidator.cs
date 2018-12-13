using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public IContext _redis;

        public DeleteCustomerCommandValidator(IContext redis)
        {
            _redis = redis;

            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.Id).Must(BeExistingCustomer).WithMessage("Customer must exist to delete");
        }

        private bool BeExistingCustomer(int id)
        {
            var customer = _redis.Cache.GetHashed<Customer>("customer:hash", $"customer:id:{id}");
            return customer != null;
        }
    }
}
