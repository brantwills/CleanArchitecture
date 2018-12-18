using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.MongoDb.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private IMongoCollection<Customer> customerCollection;

        private string GetCustomerCollectionName() => "customers";

        public CustomerRepository(IMongoDatabase db)
        {
            customerCollection = db.GetCollection<Customer>(GetCustomerCollectionName());
        }

        public async void CreateCustomer(Customer customer)
        {
            await customerCollection.InsertOneAsync(customer);
        }

        public async void DeleteCustomerById(int id)
        {
            var filter = Builders<Customer>.Filter.Eq(_ => _.CustomerId, id);
            await customerCollection.DeleteOneAsync(filter);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await customerCollection.Find(_ => _.CustomerId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await customerCollection.Find(_ => true).ToListAsync();
        }

        public async void UpdateCustomer(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(_ => _.CustomerId, customer.CustomerId);
            await customerCollection.ReplaceOneAsync(filter, customer);
        }
    }
}
