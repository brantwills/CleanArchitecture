using Akka.Actor;

namespace CleanArchitecture.AkkaNET.Interfaces
{
    public interface ICustomerActorProvider
    {
        IActorRef Get();
    }
}
