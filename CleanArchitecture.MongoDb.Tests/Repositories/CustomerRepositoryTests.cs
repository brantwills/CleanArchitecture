using CleanArchitecture.Domain.Entities;
using CleanArchitecture.MongoDb.Repositories;
using MongoDB.Driver;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace CleanArchitecture.MongoDb.Tests.Repositories
{
    [Collection("RepositoryCollection")]
    public class CustomerRepositoryTests
    {
        /*
        [Fact]
        public async void ItShouldGetCustomerById()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                CustomerName = "first last"
            };

            var context = new Mock<IMongoDatabase>();
            var collection = new Mock<IMongoCollection<Customer>>();
            var findFluent = new Mock<IFindFluent<Customer, Customer>>();

            collection.Setup(_ => _.Find(It.IsAny<FilterDefinition<Customer>>(), null)).Returns(findFluent.Object);
            context.Setup(_ => _.GetCollection<Customer>("customers", null)).Returns(collection.Object);

            var sut = new CustomerRepository(context.Object);
            var result = await sut.GetCustomerById(1);

            result.ShouldBeOfType<Customer>();
            result.ShouldBe(customer);
        }
        */
    }
}
