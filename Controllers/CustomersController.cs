using System;
using System.Net;
using customers.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Net.Mail;

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
        public JsonResult GetCustomers()
        {
            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var customers = JsonSerializer.Deserialize<Customer[]>(json);
                
                // on load giving of ids to the initial customer records by checking if second customer has a unique id

                if (customers.Length > 1 && customers[1].Id != 1)
                {
                    // update the customer ids to be based off their index in the array
                    customers = UpdateCustomerIds(customers);

                    // write the updated customers to the json file
                    var newJson = JsonSerializer.Serialize(customers);

                    System.IO.File.WriteAllText(@"DataLayer\customersData.json", newJson);
                }
                
                // return the customers as json
                return new JsonResult(customers);
            }

            return null;
        }

        [HttpGet]
        public JsonResult GetCustomer(int id)
        {
            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                if (id < customers.Length)
                {
                    return new JsonResult(customers[id]);
                }
                return new JsonResult(customers);
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

                    customers = UpdateCustomerIds(customers);

                    var newJson = JsonSerializer.Serialize(customers);

                    System.IO.File.WriteAllText(@"DataLayer\customersData.json", newJson);
                }
            }
        }

        public Customer[] UpdateCustomerIds (Customer[] customers)
        {
            var id = 0;
            // create a unique id for each customer based of their index in the array
            foreach (var customer in customers)
            {
                customer.Id = id;
                id++;

                _logger.LogInformation($"Customer: {customer.FirstName} {customer.LastName}");
            }

            return customers;
        }
        

        [HttpPost]
        public HttpStatusCode DeleteCustomer(int id)
        {
            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                var newCustomers = customers.Where((source, index) => index != id).ToArray();
                
                var newJson = JsonSerializer.Serialize(newCustomers);

                System.IO.File.WriteAllText(@"DataLayer\customersData.json", newJson);
                
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }

        [HttpPost]
        public HttpStatusCode DeleteSuppliedCustomer()
        { 
            // get the json from the request body
            var json = new StreamReader(Request.Body).ReadToEndAsync().Result;

            // convert json to customer object
            var customerSentToUs = JsonSerializer.Deserialize<Customer>(json);
            if (customerSentToUs == null)
            {
                return HttpStatusCode.BadRequest;
            }

            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var jsonFile = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var ourCustomers = JsonSerializer.Deserialize<Customer[]>(jsonFile);
                
                // check if the customer id exists
                if (customerSentToUs.Id < ourCustomers.Length)
                {
                    // remove the customer from our collection of customers
                    var newCustomers = ourCustomers.Where((source, index) => index != customerSentToUs.Id).ToArray();

                    var newJson = JsonSerializer.Serialize(newCustomers);

                    System.IO.File.WriteAllText(@"DataLayer\customersData.json", newJson);

                    return HttpStatusCode.OK;
                }
            }

            return HttpStatusCode.NotFound;
        }
        

        [HttpPost]
        public HttpStatusCode EditCustomer()
        {
            // get the json from the request body
            var json = new StreamReader(Request.Body).ReadToEndAsync().Result;

            // convert json to customer object
            var customerSentToUs = JsonSerializer.Deserialize<Customer>(json);
            if (customerSentToUs == null)
            {
                return HttpStatusCode.BadRequest;
            }

            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var jsonFile = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var customers = JsonSerializer.Deserialize<Customer[]>(jsonFile);

                // check if the customer id exists
                if (customerSentToUs.Id < customers.Length)
                {
                    // update the customer
                    customers[customerSentToUs.Id] = customerSentToUs;

                    // write the updated customers to the json file
                    var newJson = JsonSerializer.Serialize(customers);

                    System.IO.File.WriteAllText(@"DataLayer\customersData.json", newJson);            
                    
                    return HttpStatusCode.OK;
                }
            }

            return HttpStatusCode.NotFound;
        }

        [HttpPost]
        public async Task<HttpStatusCode> AddCustomer()
        {
            // get the json from the request body
            var json = new StreamReader(Request.Body).ReadToEndAsync().Result;

            // convert json to customer object
            var customer = JsonSerializer.Deserialize<Customer>(json);

            // check if customers.json file exists
            if (System.IO.File.Exists(@"DataLayer\customersData.json"))
            {
                var ourCustomersJson = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                var customers = JsonSerializer.Deserialize<Customer[]>(ourCustomersJson);

                // add the new customer to the array
                var newCustomers = new Customer[customers.Length + 1];

                for (var i = 0; i < customers.Length; i++)
                {
                    newCustomers[i] = customers[i];
                }

                //  give the new customer relevant id
                customer.Id = customers.Length;

                newCustomers[customers.Length] = customer;

                // write the updated customers to the json file
                var newCustomersJson = JsonSerializer.Serialize(newCustomers);

                System.IO.File.WriteAllText(@"DataLayer\customersData.json", newCustomersJson);

                return HttpStatusCode.OK;
            }

            return HttpStatusCode.NotFound;
        }
    }
}
