using System.Threading;
using Akka.Actor;
using Akka.TestKit.Xunit2;
using CleanArchitecture.AkkaNET.Actors;
using CleanArchitecture.AkkaNET.Interfaces;
using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Domain.Interfaces;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.Tests.Customers.Commands
{
    [Collection("CommandCollection")]
    public class CreateCustomerCommandHandlerTests : TestKit
    {
        [Fact]
        public async void ItShouldCreateCustomer()
        {
            var probe = CreateTestProbe();
            var context = new Mock<ICustomerRepository>();
            var provider = new Mock<ICustomerActorProvider>();
            var actor = Sys.ActorOf(Props.Create<CustomersActor>(context), "customers");
            var customerCommand = new CreateCustomerCommand { Id = 1, FirstName = "first", LastName = "last" };

            provider.Setup(_ => _.Get()).Returns(actor);
            var sut = new CreateCustomerCommandHandler(provider.Object);
            var result = await sut.Handle(customerCommand, CancellationToken.None);

            result.ShouldBeOfType<MediatR.Unit>();
        }
    }
}
