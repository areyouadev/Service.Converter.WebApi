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

    public class UserTests
        : IClassFixture<DefaultTestFixture>
    {
        private readonly DefaultTestFixture _factory;
        private static readonly Random random = new Random();

        public UserTests(DefaultTestFixture factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Can_Create_User()
        {
            string url = "v1/Users";
            // Arrange
            var client = _factory.CreateClient();

            var userName = RandomString(5);
            var userEmail = $"{userName}@gmail.com";
            var userPassword = RandomString(8);

            var command = new CreateCommand(userName, userEmail, userPassword, userPassword);
            // Act
            var response = await client.PostAsJsonAsync(url, command);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Can_Create_User_And_Authenticate()
        {
            string url = "v1/Users";
            // Arrange
            var client = _factory.CreateClient();

            var userName = RandomString(5);
            var userEmail = $"{userName}@gmail.com";
            var userPassword = RandomString(8);

            var command = new CreateCommand(userName, userEmail, userPassword, userPassword);
        
            // Act
            var response = await client.PostAsJsonAsync(url, command);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string authUrl = "v1/Auth";
            var authCommand = new AuthCommand(userEmail, userPassword);

            // Act
            var authResponseMessage = await client.PostAsJsonAsync(authUrl, authCommand);

            authResponseMessage.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, authResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Can_Get_User_By_Id()
        {
            var client = _factory.CreateClient();
            var userName = RandomString(5);
            var userPassword = RandomString(8);
            var userEmail = $"{userName}@gmail.com";
            var userId = await GetUser(userName, userPassword, userEmail);

            var token = await GetToken(userEmail, userPassword);

            string getUserUrl = $"v1/Users/{userId}";

            // Act
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
            var userResponseMessage = await client.GetAsync(getUserUrl);

            // Assert
            userResponseMessage.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, userResponseMessage.StatusCode);
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

        public User Convert(object jsonString)
        {
            JObject jObject = JObject.Parse(jsonString.ToString());
            var name = (string)jObject["username"];
            var email = (string)jObject["email"];
            var id = (string)jObject["id"];
            return new User(name, email, string.Empty);
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
