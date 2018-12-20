using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entities;
using Moq;
using Xunit;
using Shouldly;

namespace CleanArchitecture.Application.Tests.Customers.Commands
{
    public class CreateCustomerCommandValidatorTests
    {
        [Fact]
        public void ItShouldNotCreateIfExists()
        {
            var customer = new Customer { CustomerId = 1 };
            var readStore = new Mock<ICustomerRepository>();
            readStore.Setup(_ => _.GetCustomerById(It.IsAny<int>())).ReturnsAsync(customer);

            var sut = new CreateCustomerCommandValidator(readStore.Object);
            var result = sut.Validate(new CreateCustomerCommand
            {
                Id = 1,
                FirstName = "first",
                LastName = "last",
            });

            result.IsValid.ShouldBe(false);
            result.Errors[0].ErrorMessage.ShouldBe("Customer already exists");
        }

        [Fact]
        public void ItShouldCreateIfNewCustomer()
        {
            Customer customer = null;
            var readStore = new Mock<ICustomerRepository>();
            readStore.Setup(_ => _.GetCustomerById(It.IsAny<int>())).ReturnsAsync(customer);

            var sut = new CreateCustomerCommandValidator(readStore.Object);
            var result = sut.Validate(new CreateCustomerCommand
            {
                Id = 1,
                FirstName = "first",
                LastName = "last",
            });

            result.IsValid.ShouldBe(true);
        }
    }
}
