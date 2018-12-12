using Akka.Actor;
using CleanArchitecture.AkkaNET.Commands;
using CleanArchitecture.AkkaNET.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private IActorRef _customerActor;

        public DeleteCustomerCommandHandler(ICustomerActorProvider customerActorProvider)
        {
            _customerActor = customerActorProvider.Get();
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            _customerActor.Tell(new CustomerCommands.DeleteCustomer(request.Id));
            return await Task.FromResult(Unit.Value);
        }
    }
}
