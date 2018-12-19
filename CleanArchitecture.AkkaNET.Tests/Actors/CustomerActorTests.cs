using Akka.Actor;
using Akka.TestKit.Xunit2;
using CleanArchitecture.AkkaNET.Commands;
using CleanArchitecture.AkkaNET.Providers;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using Moq;
using Xunit;

namespace CleanArchitecture.AkkaNET.Tests.Actors
{

    public class CustomerActorTests : TestKit
    {
        [Fact]
        public void ItShouldTellCreateCommand()
        {
            var context = new Mock<ICustomerRepository>();
            var provider = new CustomerActorProvider(Sys, context.Object);
            context.Setup(_ => _.CreateCustomer(It.IsAny<Customer>()));

            var actor = provider.Get();
            actor.Tell(new CustomerCommands.CreateCustomer(1, "first", "last"));
        }
    }
}
