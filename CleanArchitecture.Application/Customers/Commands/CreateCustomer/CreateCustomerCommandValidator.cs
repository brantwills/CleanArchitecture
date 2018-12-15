using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.RedisDb;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator  : AbstractValidator<CreateCustomerCommand>
    {
        private IReadStoreHandler _redis;


        public CreateCustomerCommandValidator(IReadStoreHandler redis)
        {
            _redis = redis;

            RuleFor(v => v.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(v => v.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("ID must be greater than zero");
            RuleFor(v => v.Id).Must(NotBeExistingCustomer).WithMessage("Customer already exists");
        }

        private bool NotBeExistingCustomer(int id)
        {
            var customer = _redis.GetById<Customer>(id);
            return customer == null;
        }
    }
}
