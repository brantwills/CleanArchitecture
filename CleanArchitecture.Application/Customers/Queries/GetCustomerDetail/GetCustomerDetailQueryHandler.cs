using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, Customer>
    {
        private IReadStoreHandler _readStore;

        public GetCustomerDetailQueryHandler(IReadStoreHandler readStore)
        {
            _readStore = readStore;
        }

        public async Task<Customer> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            return await _readStore.GetById<Customer>(request.Id);
        }
    }
}
