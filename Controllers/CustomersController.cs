using customers.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace customers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
