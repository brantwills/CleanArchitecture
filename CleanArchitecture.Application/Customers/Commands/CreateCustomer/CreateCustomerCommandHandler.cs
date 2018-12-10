using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using CleanArchitecture.AkkaNET.Commands;
using CleanArchitecture.AkkaNET.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
    {
        private IActorRef _customerActor;

        public CreateCustomerCommandHandler(ICustomerActorProvider customerActorProvider)
        {
            _customerActor = customerActorProvider.Get();
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            _customerActor.Tell(new CustomerCommands.CreateCustomer(
                request.Id, 
                request.FirstName, 
                request.LastName
            ));
            return await Task.FromResult(Unit.Value);
        }
    }
}
