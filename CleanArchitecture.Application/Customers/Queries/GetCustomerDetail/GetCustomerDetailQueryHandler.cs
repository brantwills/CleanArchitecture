using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, Customer>
    {
        private IContext _redis;

        public GetCustomerDetailQueryHandler(IContext redis)
        {
            _redis = redis;
        }

        public async Task<Customer> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            return await _redis.Cache.FetchHashedAsync<Customer>("customer:hash", $"customer:id:{request.Id}", () => null);
        }
    }
}
