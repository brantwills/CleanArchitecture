using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.RedisDb.Repositories;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.RedisDb.Tests
{
    [Collection("RepositoryCollection")]
    public class CustomerRepositoryTests
    {
        [Fact]
        public async void ItShouldGetCustomerById()
        {
            var customer = new Customer {
                CustomerId = 1,
                CustomerName = "first last"
            };

            var context = new Mock<IContext>();
            context.Setup(_ => _.Cache.FetchHashedAsync(
                "customer:hash",
                "customer:id:1",
                It.IsAny<Func<Task<Customer>>>(), 
                null)
            ).ReturnsAsync(customer);

            var sut = new CustomerRepository(context.Object);
            var result = await sut.GetCustomerById(1);

            result.ShouldBeOfType<Customer>();
            result.ShouldBe(customer);
        }

        [Fact]
        public async void ItShouldGetCustomers()
        {
            var customer1 = new Customer { CustomerId = 1, CustomerName = "first last" };
            var customer2 = new Customer { CustomerId = 2, CustomerName = "test test" };
            var customers = new List<Customer> { customer1, customer2 };
            var customersHashed = new Dictionary<string, Customer>();
            customersHashed.Add("customer:id:1", customer1);
            customersHashed.Add("customer:id:2", customer2);

            var context = new Mock<IContext>();
            context.Setup(_ => _.Cache
                .GetHashedAllAsync<Customer>("customer:hash"))
                .ReturnsAsync(customersHashed);

            var sut = new CustomerRepository(context.Object);
            var result = await sut.GetCustomers();

            result.ShouldBe(customers);
        }

        [Fact]
        public void ItShouldUpdateAndCreateCustomer()
        {
            var customer = new Customer {
                CustomerId = 1,
                CustomerName = "updated customer"
            };

            var context = new Mock<IContext>();
            context.Setup(_ => _.Cache.SetHashedAsync(
                "customer:hash",
                "customer:id:1",
                It.IsAny<Customer>(),
                It.IsAny<string[]>(),
                It.IsAny<TimeSpan>(),
                It.IsAny<When>())
            );

            var sut = new CustomerRepository(context.Object);
            sut.CreateCustomer(customer);
            sut.UpdateCustomer(customer);
        }

        [Fact]
        public void ItShouldDeleteCustomer()
        {
            var context = new Mock<IContext>();
            context.Setup(_ => _.Cache
                .RemoveHashedAsync("customer:hash", "customer:id:1"))
                .ReturnsAsync(true);

            var sut = new CustomerRepository(context.Object);
            sut.DeleteCustomerById(1);
        }
    }
}
