using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.RedisDb;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public IReadStoreHandler _redis;

        public DeleteCustomerCommandValidator(IReadStoreHandler redis)
        {
            _redis = redis;

            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.Id).Must(BeExistingCustomer).WithMessage("Customer must exist to delete");
        }

        private bool BeExistingCustomer(int id)
        {
            var customer = _redis.GetById<Customer>(id);
            return customer != null;
        }
    }
}
