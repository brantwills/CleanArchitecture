using CleanArchitecture.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(int id);

        Task<IEnumerable<Customer>> GetCustomers();

        void CreateCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomerById(int id);
    }
}
