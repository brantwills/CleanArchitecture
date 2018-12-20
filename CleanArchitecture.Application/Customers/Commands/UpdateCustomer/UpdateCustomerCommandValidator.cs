using CleanArchitecture.Domain.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public  ICustomerRepository _readStore;

        public UpdateCustomerCommandValidator(ICustomerRepository readStore)
        {
            _readStore = readStore;

            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
            RuleFor(v => v.Id).MustAsync(BeExistingCustomer).WithMessage("Customer must exist to update");
        }

        private async Task<bool> BeExistingCustomer(int id, CancellationToken token)
        {
            var customer = await _readStore.GetCustomerById(id);
            return customer != null;
        }
    }
}
