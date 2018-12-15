using System.Threading;
using System.Threading.Tasks;
using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.RedisDb;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomersList
{
    class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, GetCustomersListQueryViewModel>
    {
        private IReadStoreHandler _redis;

        public GetCustomersListQueryHandler(IReadStoreHandler redis)
        {
            _redis = redis;
        }

        public async Task<GetCustomersListQueryViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _redis.Get<Customer>();
            return new GetCustomersListQueryViewModel() { Customers = customers };
        }
    }
}
