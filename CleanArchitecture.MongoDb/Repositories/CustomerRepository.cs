using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.MongoDb.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private IMongoDatabase _db;

        public CustomerRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public void CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var collection = _db.GetCollection<Customer>("customers");
            return await collection.Find(_ => true).ToListAsync();
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
