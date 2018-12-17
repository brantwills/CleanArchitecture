using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, GetCustomersListQueryViewModel>
    {
        private ICustomerRepository  _readStore;

        public GetCustomersListQueryHandler(ICustomerRepository readStore)
        {
            _readStore = readStore;
        }

        public async Task<GetCustomersListQueryViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _readStore.GetCustomers();
            return new GetCustomersListQueryViewModel() { Customers = customers };
        }
    }
}
