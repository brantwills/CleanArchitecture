using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public  ICustomerRepository _readStore;

        public UpdateCustomerCommandValidator(ICustomerRepository readStore)
        {
            _readStore = readStore;

            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
            RuleFor(v => v.Id).Must(BeExistingCustomer).WithMessage("Customer must exist to update");
        }

        private bool BeExistingCustomer(int id)
        {
            var customer = _readStore.GetCustomerById(id);
            return customer != null;
        }
    }
}
