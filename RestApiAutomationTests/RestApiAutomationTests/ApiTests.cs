using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Legacy;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiAutomationTests
{
    public class ApiTests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            // Initialize RestClient with base URL
            _client = new RestClient("https://jsonplaceholder.typicode.com");
        }

        [Test]   // Should Return ListOfUsers
        public async Task GetUsers()
        {
            var request = new RestRequest("/users", Method.Get);  // Arrange
            var response = await _client.ExecuteAsync(request);   // Act
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Expected status code 200."); // Assert

            // Parse JSON response body
            var content = response.Content;
            var jsonResponse = JArray.Parse(content);

            // Verify the number of users returned is greater than 0
            Assert.That(jsonResponse.Count, Is.GreaterThan(0), "Expected more than 0 users.");
            
            // Further validation: Check the structure of the first user
            var firstUser = jsonResponse[0];
            Assert.Multiple(() =>
            {
                Assert.That(firstUser["id"], Is.Not.EqualTo(null), "Expected 'id' in user data.");
                Assert.That(firstUser["name"], Is.Not.EqualTo(null), "Expected 'name' in user data.");
            });

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);

            ExtentTestManager.GetTest().Info(MarkupHelper.CreateCodeBlock(response.Content, CodeLanguage.Json));


        }



        [Test]   // _ShouldReturnCreatedUser
        public async Task CreateUser()
        {
            // Arrange
            var newUser = new
            {
                name = "John Doe",
                email = "john.doe@example.com",
                username = "johnny"
            };

            var request = new RestRequest("/users", Method.Post);
            request.AddJsonBody(newUser); // Add the user data to the request body

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            ClassicAssert.AreEqual(201, (int)response.StatusCode, "Expected status code 201 for created resource.");

            // Parse JSON response to verify the user creation
            var content = response.Content;
            var jsonResponse = JObject.Parse(content);

            ClassicAssert.AreEqual(newUser.name, jsonResponse["name"].ToString(), "User name mismatch.");
            ClassicAssert.AreEqual(newUser.email, jsonResponse["email"].ToString(), "User email mismatch.");

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);

        }

        [TearDown]
        public void AfterTest()
        { 
            _client.Dispose(); 
        }


    }
}
