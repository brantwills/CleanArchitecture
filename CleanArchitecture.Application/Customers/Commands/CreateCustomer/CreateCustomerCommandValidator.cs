using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.RedisDb;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator  : AbstractValidator<CreateCustomerCommand>
    {
        private IContext _redis;


        public CreateCustomerCommandValidator(IContext redis)
        {
            _redis = redis;

            RuleFor(v => v.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(v => v.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("ID must be greater than zero");
            RuleFor(v => v.Id).Must(NotBeExistingCustomer).WithMessage("Customer already exists");
        }

        private bool NotBeExistingCustomer(int id)
        {
            var customer = _redis.Cache.GetHashed<Customer>(RedisLookup.Customer.GetHashKey(), RedisLookup.Customer.GetHashField(id));
            return customer == null;
        }
    }
}
