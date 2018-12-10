using CleanArchitecture.AkkaNET.Commands;

namespace CleanArchitecture.AkkaNET.Events
{
    public class CustomerEvents
    {
        public class CustomerCreated : CustomerCommands.CreateCustomer
        {
            public CustomerCreated(int id, string firstname, string lastname) : base(id, firstname, lastname) { }
        }

        public class CustomerUpdated : CustomerCommands.UpdateCustomer
        {
            public CustomerUpdated(int id, string firstname, string lastname) : base(id, firstname, lastname) { }
        }
    }
}
