using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace M2c.Api.Application.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private readonly string _connectionString = string.Empty;

        public CustomerQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr)
                ? constr
                : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<CustomerSummary>> GetCustomersAsync(string firstName, string lastname)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                IEnumerable<dynamic> result = await connection.QueryAsync<dynamic>(
                    @"select [Firstname],[Lastname],[DateOfBirth],[PhoneNumber],[Email],[BankAccountNumber] 
                        from [M2C].Customers
                        where [Deleted]=0
                        and( ([Firstname]=@firstname )
                        or ([Lastname]=@lastname ))", new { firstName, lastname });
                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();
                return MapCustomers(result);
            }
        }

        private IEnumerable<CustomerSummary> MapCustomers(dynamic result)
        {
            List<CustomerSummary> customers = new();
            foreach (dynamic item in result)
            {
                CustomerSummary customer = new()
                {
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    DateOfBirth = item.DateOfBirth,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    BankAccountNumber = item.BankAccountNumber
                };
                customers.Add(customer);
            }

            return customers;
        }
    }
}