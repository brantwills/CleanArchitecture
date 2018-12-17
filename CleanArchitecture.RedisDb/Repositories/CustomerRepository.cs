using CachingFramework.Redis.Contracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.RedisDb.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private IContext _redis;

        private string GetCustomerHash() => "customer:hash";

        private string GetCustomerField(int id) => $"customer:id:{id}";

        public CustomerRepository(IContext redis)
        {
            _redis = redis;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _redis.Cache.FetchHashedAsync<Customer>(
                GetCustomerHash(), 
                GetCustomerField(id), 
                () => null, 
                null);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var tList = await _redis.Cache.GetHashedAllAsync<Customer>(GetCustomerHash());
            return tList.Select(i => i.Value);
        }

        public async void CreateCustomer(Customer item)
        {
            await _redis.Cache.SetHashedAsync(
                GetCustomerHash(), 
                GetCustomerField(item.CustomerId), 
                item);
        }

        public void UpdateCustomer(Customer customer)
        {
            CreateCustomer(customer);
        }

        public async void DeleteCustomerById(int id)
        {
            await _redis.Cache.RemoveHashedAsync(
                GetCustomerHash(), 
                GetCustomerField(id));
        }

    }
}
