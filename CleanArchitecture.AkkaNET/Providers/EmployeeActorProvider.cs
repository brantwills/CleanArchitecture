using Akka.Actor;
using CleanArchitecture.AkkaNET.Actors;
using CleanArchitecture.AkkaNET.Interfaces;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.AkkaNET.Providers
{
    public class EmployeeActorProvider : IEmployeeActorProvider
    {
        private IActorRef EmployeeActor { get; set; }

        public EmployeeActorProvider(IActorRefFactory actorSystem, ICustomerRepository repo)
        {
            EmployeeActor = actorSystem.ActorOf(Props.Create<CustomersActor>(repo), "employees");
        }

        public IActorRef Get()
        {
            return EmployeeActor;
        }
    }
}
