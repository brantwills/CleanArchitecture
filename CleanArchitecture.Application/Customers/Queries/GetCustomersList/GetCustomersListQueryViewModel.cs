using CleanArchitecture.Domain.Entities;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQueryViewModel
    {
        public IDictionary<string, Customer> Customers { get; set; }
    }
}
