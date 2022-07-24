using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;


namespace M2c.Api.Application.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private string _connectionString = string.Empty;

        public CustomerQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr)
                ? constr
                : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<CustomerSummary>> GetCustomersAsync(string firstName, string lastname)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    sql: @"select [Firstname],[Lastname],[DateOfBirth],[PhoneNumber],[Email],[BankAccountNumber] 
                        from [M2C].Customers
                        where [Deleted]=0
                        and( ([Firstname]=@firstname )
                        or ([Lastname]=@lastname ))", new { firstName,lastname});
                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();
                return MapCustomers(result);
            }
            
        }

        private IEnumerable<CustomerSummary> MapCustomers(dynamic result)
        {
            var customers = new List<CustomerSummary>();
            foreach (dynamic item in result)
            {
                var customer = new CustomerSummary
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