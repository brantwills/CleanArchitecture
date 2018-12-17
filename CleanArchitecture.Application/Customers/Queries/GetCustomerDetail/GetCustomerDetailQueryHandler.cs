using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, Customer>
    {
        private ICustomerRepository _readStore;

        public GetCustomerDetailQueryHandler(ICustomerRepository readStore)
        {
            _readStore = readStore;
        }

        public async Task<Customer> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            return await _readStore.GetCustomerById(request.Id);

        }
    }
}
