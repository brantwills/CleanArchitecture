using Akka.Actor;
using CachingFramework.Redis.Contracts;
using CleanArchitecture.AkkaNET.Actors;
using CleanArchitecture.AkkaNET.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.AkkaNET.Providers
{
    public class CustomerActorProvider : ICustomerActorProvider
    {
        private IActorRef CustomersActor { get; set; }

        public CustomerActorProvider(IActorRefFactory actorSystem, IContext redis)
        {
            CustomersActor = actorSystem.ActorOf(Props.Create<CustomersActor>(redis), "customers");
        }

        public IActorRef Get()
        {
            return CustomersActor;
        }
    }
}
