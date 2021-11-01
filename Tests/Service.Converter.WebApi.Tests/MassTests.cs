namespace Service.Converter.WebApi.Tests
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;

    using Xunit;
    using Newtonsoft.Json.Linq;

    using Domain.Commands;
    using Domain.Entities;
    using Domain.Commands.Users;

    public class MassTests
        : IClassFixture<DefaultTestFixture>
    {
        private readonly DefaultTestFixture _factory;
        private static readonly Random random = new Random();

        public MassTests(DefaultTestFixture factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Can_Convert_PoundToKilograms()
        {
            string url = "v1/Mass/PoundToKilograms/40";

            var client = _factory.CreateClient();
            var userName = RandomString(5);
            var userPassword = RandomString(8);
            var userEmail = $"{userName}@gmail.com";
            var userId = await GetUser(userName, userPassword, userEmail);

            var token = await GetToken(userEmail, userPassword);

            // Act
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
            var response = await client.GetAsync(url);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task Can_Convert_KilogramToPounds()
        {
            string url = "v1/Mass/KilogramToPounds/90";

            var client = _factory.CreateClient();
            var userName = RandomString(5);
            var userPassword = RandomString(8);
            var userEmail = $"{userName}@gmail.com";
            var userId = await GetUser(userName, userPassword, userEmail);

            var token = await GetToken(userEmail, userPassword);

            // Act
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
            var response = await client.GetAsync(url);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }

        private async Task<string> GetUser(string userName, string userPassword,string userEmail)
        {
            string url = "v1/Users";

            // Arrange
            var client = _factory.CreateClient();
            
            var command = new CreateCommand(userName, userEmail, userPassword, userPassword);

            // Act
            var response = await client.PostAsJsonAsync(url, command);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var genericCommandResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GenericCommandResult>(response.Content.ReadAsStringAsync().Result);
            return GetUserId(genericCommandResult?.Data);
        }
       
        private async Task<string> GetToken(string userEmail, string userPassword)
        {

            string authUrl = "v1/Auth";
            var client = _factory.CreateClient();
            var authCommand = new AuthCommand(userEmail, userPassword);

            // Act
            var authResponseMessage = await client.PostAsJsonAsync(authUrl, authCommand);

            authResponseMessage.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, authResponseMessage.StatusCode);

            var authResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthResponse>(authResponseMessage.Content.ReadAsStringAsync().Result);

            return $"bearer {authResponse?.Token}";
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GetUserId(object jsonString)
        {
            JObject jObject = JObject.Parse(jsonString.ToString());
            var name = (string)jObject["username"];
            var email = (string)jObject["email"];
            var id = (string)jObject["id"];
            return id;
        }
    }
}
