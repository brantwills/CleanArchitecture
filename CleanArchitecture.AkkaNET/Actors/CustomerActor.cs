using Akka.Persistence;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using static CleanArchitecture.AkkaNET.Commands.CustomerCommands;
using static CleanArchitecture.AkkaNET.Events.CustomerEvents;

namespace CleanArchitecture.AkkaNET.Actors
{
    public class CustomersActor : ReceivePersistentActor
    {
        private ICustomerRepository _readStore;

        public override string PersistenceId { get; }

        public CustomersActor(ICustomerRepository readStore)
        {
            _readStore = readStore;
            PersistenceId = nameof(CustomersActor);
            InitCommands();
            InitRecovery();
        }

        /// <summary>
        /// Setup the Commands
        /// </summary>
        private void InitCommands()
        {
            Command<CreateCustomer>(cmd => {
                var evt = new CustomerCreated(cmd.Id, cmd.FirstName, cmd.LastName);
                Persist(evt, e => 
                {
                    _readStore.CreateCustomer(new Customer
                    {
                        CustomerId = evt.Id,
                        CustomerName = $"{evt.FirstName} {evt.LastName}",
                    });
                    Context.System.EventStream.Publish(e);
                });
            });

            Command<UpdateCustomer>(cmd => {
                var evt = new CustomerUpdated(cmd.Id, cmd.FirstName, cmd.LastName);
                Persist(evt, e => 
                {
                    _readStore.UpdateCustomer(new Customer
                    {
                        CustomerId = evt.Id,
                        CustomerName = $"{evt.FirstName} {evt.LastName}",
                    });
                    Context.System.EventStream.Publish(e);
                });
            });

            Command<DeleteCustomer>(cmd =>
            {
                var evt = new CustomerDeleted(cmd.Id);
                Persist(evt, e => 
                {
                    _readStore.DeleteCustomerById(evt.Id);
                    Context.System.EventStream.Publish(e); 
                });
            });
        }

        /// <summary>
        /// Setup the Recovery
        /// </summary>
        private void InitRecovery()
        {
            Recover<CustomerCreated>(evt =>
            {
                _readStore.CreateCustomer(new Customer
                {
                    CustomerId = evt.Id,
                    CustomerName = $"{evt.FirstName} {evt.LastName}",
                });
            });

            Recover<CustomerUpdated>(evt =>
            {
                _readStore.UpdateCustomer(new Customer
                {
                    CustomerId = evt.Id,
                    CustomerName = $"{evt.FirstName} {evt.LastName}",
                });
            });

            Recover<CustomerDeleted>(evt =>
            {
                _readStore.DeleteCustomerById(evt.Id);
            });
        }
    }
}
