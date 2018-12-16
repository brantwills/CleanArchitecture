using CachingFramework.Redis.Contracts;
using CleanArchitecture.Application.Customers.Queries.GetCustomersList;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Customers.Queries
{
    public class GetCustomersListQueryHandlerTests
    {
        [Fact]
        public async Task GetCustomerDetail()
        {
            var customers = new List<Customer> {
                new Customer { CustomerId = 1, CustomerName = "first last" },
                new Customer { CustomerId = 2, CustomerName = "test test" },
            };

            var context = new Mock<IReadStoreHandler>();
            context.Setup(_ => _.Get<Customer>()).ReturnsAsync(customers);

            var sut = new GetCustomersListQueryHandler(context.Object);
            var result = await sut.Handle(new GetCustomersListQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetCustomersListQueryViewModel>();
            result.Customers.ShouldBe(customers);
        }
    }
}
