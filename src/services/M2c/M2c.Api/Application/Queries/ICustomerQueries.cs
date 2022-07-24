using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2c.Api.Application.Queries
{
    public interface ICustomerQueries
    {
        Task<IEnumerable<CustomerSummary>> GetCustomersAsync(string firstName,string lastname);
    }
}