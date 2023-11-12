namespace TestCustomers
{
    [TestClass]
    public class DataSourceFileTest
    {
        [TestMethod]
        public void DataSourceFileExist()
        {
            // Arrange
            // C:\Test\Customers\DataLayer\customersData.json
            const string dataSourcePath = @"DataLayer\customersData.json";

            // Act
            var result = System.IO.File.Exists(dataSourcePath);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataSourceFileNotExist()
        {
            // Arrange
            const string dataSourcePath = @"DataLayer\testFilePath.json";

            // Act
            var result = System.IO.File.Exists(dataSourcePath);

            // Assert
            Assert.IsFalse(result);
        }
    }
}