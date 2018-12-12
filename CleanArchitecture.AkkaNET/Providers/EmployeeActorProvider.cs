using Akka.Actor;
using CachingFramework.Redis.Contracts;
using CleanArchitecture.AkkaNET.Actors;
using CleanArchitecture.AkkaNET.Interfaces;

namespace CleanArchitecture.AkkaNET.Providers
{
    public class EmployeeActorProvider : IEmployeeActorProvider
    {
        private IActorRef EmployeeActor { get; set; }

        public EmployeeActorProvider(IActorRefFactory actorSystem, IContext redis)
        {
            EmployeeActor = actorSystem.ActorOf(Props.Create<CustomersActor>(redis), "employees");
        }

        public IActorRef Get()
        {
            return EmployeeActor;
        }
    }
}
