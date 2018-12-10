using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery : IRequest<Customer>
    {
        public int Id { get; set; }
    }
}
