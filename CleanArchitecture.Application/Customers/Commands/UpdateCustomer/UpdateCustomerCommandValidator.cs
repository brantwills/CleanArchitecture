using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public IContext _redis;

        public UpdateCustomerCommandValidator(IContext redis)
        {
            _redis = redis;

            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
            RuleFor(v => v.Id).Must(BeExistingCustomer).WithMessage("Customer must exist to update");
        }

        private bool BeExistingCustomer(int id)
        {
            var customer = _redis.Cache.GetHashed<Customer>("customer:hash", $"customer:id:{id}");
            return customer != null;
        }
    }
}
