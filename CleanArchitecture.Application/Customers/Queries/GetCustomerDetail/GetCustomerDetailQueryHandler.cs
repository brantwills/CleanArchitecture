using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, Customer>
    {
        private IDistributedCache _redis;

        public GetCustomerDetailQueryHandler(IDistributedCache redis)
        {
            _redis = redis;
        }

        public async Task<Customer> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var customer = await _redis.GetStringAsync($"customer:{request.Id}");
            return !string.IsNullOrWhiteSpace(customer) ? JsonConvert.DeserializeObject<Customer>(customer) : null;
        }
    }
}
