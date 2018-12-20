using CleanArchitecture.Domain.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public ICustomerRepository _readStore;

        public DeleteCustomerCommandValidator(ICustomerRepository readStore)
        {
            _readStore = readStore;

            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.Id).MustAsync(BeExistingCustomer).WithMessage("Customer must exist to delete");
        }

        private async Task<bool> BeExistingCustomer(int id, CancellationToken token)
        {
            var customer = await _readStore.GetCustomerById(id);
            return customer != null;
        }
    }
}
