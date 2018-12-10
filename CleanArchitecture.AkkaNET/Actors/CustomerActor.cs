using Akka.Persistence;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using static CleanArchitecture.AkkaNET.Commands.CustomerCommands;
using static CleanArchitecture.AkkaNET.Events.CustomerEvents;

namespace CleanArchitecture.AkkaNET.Actors
{
    public class CustomersActor : ReceivePersistentActor
    {
        private IDistributedCache _redis;

        public override string PersistenceId { get; }

        public CustomersActor(IDistributedCache redis)
        {
            _redis = redis;
            PersistenceId = $"{nameof(CustomersActor)}-{Guid.NewGuid().ToString("N")}";
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
                Persist(evt, e => {
                    // todo(bwills): investigate implemenatation and make type safe
                    _redis.SetString($"customer:{cmd.Id}", JsonConvert.SerializeObject(new {
                        CustomerId = evt.Id,
                        CustomerName = $"{evt.FirstName} {evt.LastName}",
                    }));
                    Context.System.EventStream.Publish(e);
                });
            });

            Command<UpdateCustomer>(cmd => {
                var evt = new CustomerUpdated(cmd.Id, cmd.FirstName, cmd.LastName);
                Persist(evt, e => {
                    _redis.SetString($"customer:{cmd.Id}", JsonConvert.SerializeObject(new {
                        CustomerId = evt.Id,
                        CustomerName = $"{evt.FirstName} {evt.LastName}",
                    }));
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
                _redis.SetString($"customer:{evt.Id}", JsonConvert.SerializeObject(evt));
            });

            Recover<CustomerUpdated>(evt =>
            {
                _redis.SetString($"customer:{evt.Id}", JsonConvert.SerializeObject(evt));
            });
        }
    }
}
