using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public  IReadStoreHandler _redis;

        public UpdateCustomerCommandValidator(IReadStoreHandler redis)
        {
            _redis = redis;

            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
            RuleFor(v => v.Id).Must(BeExistingCustomer).WithMessage("Customer must exist to update");
        }

        private bool BeExistingCustomer(int id)
        {
            var customer = _redis.GetById<Customer>(id);
            return customer != null;
        }
    }
}
