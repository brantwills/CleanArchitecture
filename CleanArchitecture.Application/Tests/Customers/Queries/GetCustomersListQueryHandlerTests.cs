using CachingFramework.Redis.Contracts;
using CleanArchitecture.Application.Customers.Queries.GetCustomersList;
using CleanArchitecture.Domain.Entities;
using Moq;
using Shouldly;
using System.Collections.Generic;
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
            var customers = new Dictionary<string, Customer>();
            customers.Add("customer:id:1", new Customer { CustomerId = 1, CustomerName = "first last" });
            customers.Add("customer:id:2", new Customer { CustomerId = 2, CustomerName = "test test" });

            var context = new Mock<IContext>();
            context.Setup(_ => _.Cache
                .GetHashedAllAsync<Customer>("customer:hash"))
                .ReturnsAsync(customers);

            var sut = new GetCustomersListQueryHandler(context.Object);
            var result = await sut.Handle(new GetCustomersListQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetCustomersListQueryViewModel>();
            result.Customers.ShouldBe(customers);
        }
    }
}
