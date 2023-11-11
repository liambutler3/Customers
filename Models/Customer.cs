using System;
using System.Text.Json.Serialization;

namespace customers.models
{
    public class Customer {

        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string EmailAddress { get; set; }
    }



}
