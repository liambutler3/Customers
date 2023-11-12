using customers;
using customers.models;
using System.Text.Json;

namespace TestCustomers
{
    [TestClass]
    public class DataSourceTest
    {
        [TestClass]
        public class DataTests
        {
            [TestMethod]
            public void StringValueFromJsonFile()
            {
                // Arrange
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                // Act
                var result = !string.IsNullOrEmpty(json);

                // Assert
                Assert.IsTrue(result);
            }
            [TestMethod]
            public void NoStringValueFromJsonFile()
            {
                // Arrange
                var json = string.Empty;

                // Act
                var result = !string.IsNullOrEmpty(json);

                // Assert
                Assert.IsFalse(result);
            }

            [TestMethod]
            public void JsonDeserialize()
            {
                // Arrange
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");

                // Act
                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                // Assert
                Assert.IsNotNull(customers);
            }

            [TestMethod]
            public void JsonSerialize()
            {
                // Arrange
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");
                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                // Act
                var newJson = JsonSerializer.Serialize(customers);

                // Assert
                Assert.IsNotNull(newJson);
            }

            [TestMethod]
            public void JsonSerializeEmpty()
            {
                // Arrange
                var customers = new Customer[0];

                // Act
                var newJson = JsonSerializer.Serialize(customers);

                // Assert
                Assert.IsNotNull(newJson);
            }

            [TestMethod]
            public void JsonSerializeEmptyString()
            {
                // Arrange
                var customers = new Customer[0];

                // Act
                var newJson = JsonSerializer.Serialize(customers);

                // Assert
                Assert.AreEqual("[]", newJson);
            }


            [TestMethod]
            public void JsonSerializeEmptyStringEmpty()
            {
                // Arrange
                var customers = new Customer[0];

                // Act
                var newJson = JsonSerializer.Serialize(customers);

                // Assert
                Assert.AreEqual("[]", newJson);
            }

            [TestMethod]
            public void GotCustomer()
            {
                // Arrange
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");
                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                // Act
                var customer = customers[0];

                // Assert
                Assert.IsNotNull(customer);
            }

            [TestMethod]
            public void GotCustomerFirstName()
            {
                // Arrange
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");
                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                // Act
                var customer = customers[0];

                // Assert
                Assert.AreEqual("David", customer.FirstName);
            }

            [TestMethod]
            public void CustomerFirstNameDoesntExist()
            {
                // Arrange
                var json = System.IO.File.ReadAllText(@"DataLayer\customersData.json");
                var customers = JsonSerializer.Deserialize<Customer[]>(json);

                // Act
                var customer = customers[0];

                // Assert
                Assert.AreNotSame("FirstName", customer.FirstName);
            }
        }
    }
}