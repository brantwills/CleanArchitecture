using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public IReadStoreHandler _readStore;

        public DeleteCustomerCommandValidator(IReadStoreHandler readStore)
        {
            _readStore = readStore;

            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.Id).Must(BeExistingCustomer).WithMessage("Customer must exist to delete");
        }

        private bool BeExistingCustomer(int id)
        {
            var customer = _readStore.GetById<Customer>(id);
            return customer != null;
        }
    }
}
