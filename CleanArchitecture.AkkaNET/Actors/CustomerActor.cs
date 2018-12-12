using Akka.Persistence;
using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using static CleanArchitecture.AkkaNET.Commands.CustomerCommands;
using static CleanArchitecture.AkkaNET.Events.CustomerEvents;

namespace CleanArchitecture.AkkaNET.Actors
{
    public class CustomersActor : ReceivePersistentActor
    {
        private IContext _redis;

        public override string PersistenceId { get; }

        public CustomersActor(IContext redis)
        {
            _redis = redis;
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
                    _redis.Cache.SetHashed("customer:hash", $"customer:id:{evt.Id}", new Customer
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
                    _redis.Cache.SetHashed("customer:hash", $"customer:id:{evt.Id}", new Customer
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
                    _redis.Cache.RemoveHashed("customer:hash", $"customer:id:{evt.Id}");
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
                _redis.Cache.SetHashed("customer:hash", $"customer:id:{evt.Id}", new Customer
                {
                    CustomerId = evt.Id,
                    CustomerName = $"{evt.FirstName} {evt.LastName}",
                });
            });

            Recover<CustomerUpdated>(evt =>
            {
                _redis.Cache.SetHashed("customer:hash", $"customer:id:{evt.Id}", new Customer
                {
                    CustomerId = evt.Id,
                    CustomerName = $"{evt.FirstName} {evt.LastName}",
                });
            });

            Recover<CustomerDeleted>(evt =>
            {
                _redis.Cache.RemoveHashed("customer:hash", $"customer:id:{evt.Id}");
            });
        }
    }
}
