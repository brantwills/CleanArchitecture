using System.Threading;
using System.Threading.Tasks;
using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomersList
{
    class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, GetCustomersListQueryViewModel>
    {
        private IContext _redis;

        public GetCustomersListQueryHandler(IContext redis)
        {
            _redis = redis;
        }

        public async Task<GetCustomersListQueryViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _redis.Cache.GetHashedAllAsync<Customer>("customer:hash");
            return new GetCustomersListQueryViewModel() { Customers = customers.Values };
        }
    }
}
