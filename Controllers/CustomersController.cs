using System;
using customers.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace customers.Controllers
{
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IConfiguration _configuration;

        public CustomersController(ILogger<CustomersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<Customer[]> Get()
        {
            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var customers = JsonSerializer.Deserialize<Customer[]>(json);
                return customers;
            }

            return null;
        }

        [HttpGet]
        public async Task<Customer[]> GetCustomer(int id)
        {
            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                if (id < customers.Length)
                {
                    return new Customer[] { customers[id] };
                }
                return customers;
            }

            return null;
        }

        [HttpPost]
        public async void Edit(int id, string first_name, string last_name, string email, string phone)
        {
            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                if (id < customers.Length)
                {
                    customers[id].FirstName = first_name;
                    customers[id].LastName = last_name;
                    customers[id].EmailAddress = email;
                    customers[id].Phone = phone;

                    var newJson = JsonSerializer.Serialize(customers);

                    System.IO.File.WriteAllText(@"DataLayer\customersData.json", newJson);
                }
            }

        }
    }
}
