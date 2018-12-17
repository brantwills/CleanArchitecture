using CleanArchitecture.Application.Customers.Queries.GetCustomerDetail;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomerDetailQueryHandlerTests
    {
        [Fact]
        public async Task ItShouldGetCustomerDetail()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                CustomerName = "first last"
            };

            var context = new Mock<ICustomerRepository>();
            context.Setup(_ => _.GetCustomerById(It.IsAny<int>())).ReturnsAsync(customer);

            var sut = new GetCustomerDetailQueryHandler(context.Object);
            var result = await sut.Handle(new GetCustomerDetailQuery{ Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<Customer>();
            result.ShouldBe(customer);
        }
    }
}
