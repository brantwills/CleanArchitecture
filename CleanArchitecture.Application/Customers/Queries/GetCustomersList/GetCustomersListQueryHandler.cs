using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomersList
{
    class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, GetCustomersListQueryViewModel>
    {
        private IReadStoreHandler _readStore;

        public GetCustomersListQueryHandler(IReadStoreHandler readStore)
        {
            _readStore = readStore;
        }

        public async Task<GetCustomersListQueryViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _readStore.Get<Customer>();
            return new GetCustomersListQueryViewModel() { Customers = customers };
        }
    }
}
