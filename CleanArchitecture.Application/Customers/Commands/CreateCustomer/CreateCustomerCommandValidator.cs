using CleanArchitecture.Domain.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator  : AbstractValidator<CreateCustomerCommand>
    {
        private ICustomerRepository _readStore;


        public CreateCustomerCommandValidator(ICustomerRepository readStore)
        {
            _readStore = readStore;

            RuleFor(v => v.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(v => v.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("ID must be greater than zero");
            RuleFor(v => v.Id).Must(NotBeExistingCustomer).WithMessage("Customer already exists");
        }

        private bool NotBeExistingCustomer(int id)
        {
            var customer = _readStore.GetCustomerById(id);
            return customer == null;
        }
    }
}
