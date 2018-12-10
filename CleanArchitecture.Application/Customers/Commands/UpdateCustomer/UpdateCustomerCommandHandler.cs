using Akka.Actor;
using CleanArchitecture.AkkaNET.Commands;
using CleanArchitecture.AkkaNET.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
    {
        private IActorRef _customerActor;

        public UpdateCustomerCommandHandler(ICustomerActorProvider provider)
        {
            _customerActor = provider.Get();
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            _customerActor.Tell(new CustomerCommands.UpdateCustomer(
                request.Id,
                request.FirstName,
                request.LastName
            ));
            return await Task.FromResult(Unit.Value);
        }
    }
}
