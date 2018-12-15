using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.RedisDb;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, Customer>
    {
        private IReadStoreHandler _read;

        public GetCustomerDetailQueryHandler(IReadStoreHandler read)
        {
            _read = read;
        }

        public async Task<Customer> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetById<Customer>(request.Id);
        }
    }
}
