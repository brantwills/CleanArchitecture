namespace CleanArchitecture.AkkaNET.Commands
{
    public class CustomerCommands
    {
        /// <summary>
        /// Command Create Customer
        /// </summary>
        public class CreateCustomer
        {
            public int Id { get; }
            public string FirstName { get; }
            public string LastName { get; }

            public CreateCustomer(int id, string firstname, string lastname)
            {
                Id = id;
                FirstName = firstname;
                LastName = lastname;
            }
        }

        /// <summary>
        /// Command Update Customer
        /// </summary>
        public class UpdateCustomer 
        {
            public int Id { get; }
            public string FirstName { get; }
            public string LastName { get; }

            public UpdateCustomer(int id, string firstname, string lastname)
            {
                Id = id;
                FirstName = firstname;
                LastName = lastname;
            }
        }
    }
}
