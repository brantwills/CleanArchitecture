using MediatR;
namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
