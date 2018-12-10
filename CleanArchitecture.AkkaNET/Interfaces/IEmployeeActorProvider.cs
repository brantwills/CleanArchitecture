using Akka.Actor;

namespace CleanArchitecture.AkkaNET.Interfaces
{
    public interface IEmployeeActorProvider
    {
        IActorRef Get();
    }
}
