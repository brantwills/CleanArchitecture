using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public ICustomerRepository _readStore;

        public DeleteCustomerCommandValidator(ICustomerRepository readStore)
        {
            _readStore = readStore;

            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.Id).Must(BeExistingCustomer).WithMessage("Customer must exist to delete");
        }

        private bool BeExistingCustomer(int id)
        {
            var customer = _readStore.GetCustomerById(id);
            return customer != null;
        }
    }
}
