namespace Service.Converter.WebApi.Tests
{
    using System.Net;
    using System.Threading.Tasks;

    using Xunit;

    public class BasicTests
        : IClassFixture<DefaultTestFixture>
    {
        private readonly DefaultTestFixture _factory;

        public BasicTests(DefaultTestFixture factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Can_Query()
        {
            string url = "v1/Health";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
