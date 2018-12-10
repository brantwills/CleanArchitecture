using Akka.Actor;
using CleanArchitecture.AkkaNET.Actors;
using CleanArchitecture.AkkaNET.Interfaces;

namespace CleanArchitecture.AkkaNET.Providers
{
    public class EmployeeActorProvider : IEmployeeActorProvider
    {
        private IActorRef EmployeeActor { get; set; }

        public EmployeeActorProvider(IActorRefFactory actorSystem)
        {
            EmployeeActor = actorSystem.ActorOf(Props.Create<CustomersActor>(), "employees");
        }

        public IActorRef Get()
        {
            return EmployeeActor;
        }
    }
}
