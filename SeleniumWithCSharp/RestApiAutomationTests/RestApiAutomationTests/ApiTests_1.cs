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
    public class ApiTests_1 : BaseClass
    {

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

        }


        [Test]   // Should Return Single User
        public async Task GetSingleUser()
        {
            var userId = 1; // Example: Fetch the user with ID 1
            var request = new RestRequest($"/users/{userId}", Method.Get); // Arrange
            var response = await _client.ExecuteAsync(request);   // Act

            ClassicAssert.AreEqual(200, (int)response.StatusCode, "Expected status code 200.");

            // Parse JSON response body
            var content = response.Content;
            var jsonResponse = JObject.Parse(content);

            // Validate the presence of 'id', 'name' and 'email' in the user data
            ClassicAssert.AreEqual(userId, jsonResponse["id"].ToObject<int>(), "User ID mismatch.");
            ClassicAssert.IsTrue(jsonResponse["name"] != null, "Expected 'name' in user data.");
            ClassicAssert.IsTrue(jsonResponse["email"] != null, "Expected 'email' in user data.");

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);
        }



        [Test]   // Should Update User
        public async Task UpdateUser()
        {
            var userId = 1;  // Example: Updating the user with ID 1
            var updatedUser = new
            {
                name = "Jane Doe",
                email = "jane.doe@example.com",
                username = "janedoe"
            };

            var request = new RestRequest($"/users/{userId}", Method.Put);
            request.AddJsonBody(updatedUser);  // Add the updated user data to the request body

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            ClassicAssert.AreEqual(200, (int)response.StatusCode, "Expected status code 200 for successful update.");

            // Parse JSON response to verify the user update
            var content = response.Content;
            var jsonResponse = JObject.Parse(content);

            ClassicAssert.AreEqual(updatedUser.name, jsonResponse["name"].ToString(), "User name mismatch.");
            ClassicAssert.AreEqual(updatedUser.email, jsonResponse["email"].ToString(), "User email mismatch.");

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);
        }



        [Test]   // Should Delete User
        public async Task DeleteUser()
        {
            var userId = 1;  // Example: Deleting the user with ID 1
            var request = new RestRequest($"/users/{userId}", Method.Delete); // Arrange

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            ClassicAssert.AreEqual(200, (int)response.StatusCode, "Expected status code 200 for successful deletion.");

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);
        }




        [Test]   // Should Return Not Found For Non ExistentUser
        public async Task GetNonExistentUser()
        {
            var userId = 9999; // Example: Fetching a non-existent user
            var request = new RestRequest($"/users/{userId}", Method.Get); // Arrange
            var response = await _client.ExecuteAsync(request);   // Act

            ClassicAssert.AreEqual(404, (int)response.StatusCode, "Expected status code 404 for non-existent user.");

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);
        }


        [Test]   // ShouldReturnBadRequestForInvalidUser
        public async Task CreateInvalidUser()
        {
            var invalidUser = new
            {
                name = "",   // Empty name (invalid data)
                email = "",  // Empty email (invalid data)
            };

            var request = new RestRequest("/users", Method.Post);
            request.AddJsonBody(invalidUser); // Add invalid user data to the request body

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            ClassicAssert.AreEqual(400, (int)response.StatusCode, "Expected status code 400 for invalid request.");

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);
        }


        [Test]   // ShouldReturnResponseWithinAcceptableTime
        public async Task VerifyResponseTime()
        {
            var request = new RestRequest("/users", Method.Get);  // Arrange

            // Capture the start time before sending the request
            var startTime = DateTime.Now;

            var response = await _client.ExecuteAsync(request);   // Act

            // Capture the end time after receiving the response
            var endTime = DateTime.Now;

            // Calculate the response time in milliseconds
            var responseTime = (endTime - startTime).TotalMilliseconds;

            // Assert: Check if the response time is less than 2000 ms
            ClassicAssert.Less(responseTime, 2000, "Response time exceeded acceptable limit.");

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Time: " + responseTime + " ms");
            Console.WriteLine("Response Content: " + response.Content);
        }



        [Test]   // ShouldValidateResponseHeaders
        public async Task ValidateResponseHeaders()
        {
            var request = new RestRequest("/users", Method.Get);  // Arrange
            var response = await _client.ExecuteAsync(request);   // Act

            ClassicAssert.IsTrue(response.Headers.Any(h => h.Name == "Content-Type" && h.Value.ToString().Contains("application/json")),
                                 "Expected 'Content-Type' header to be 'application/json'.");

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Headers: " + string.Join(", ", response.Headers.Select(h => $"{h.Name}: {h.Value}")));
        }


        [Test]   // ShouldValidateUserEmailFormat
        public async Task ValidateUserEmailFormat()
        {
            var request = new RestRequest("/users", Method.Get);  // Arrange
            var response = await _client.ExecuteAsync(request);   // Act

            ClassicAssert.AreEqual(200, (int)response.StatusCode, "Expected status code 200.");

            var content = response.Content;
            var jsonResponse = JArray.Parse(content);

            foreach (var user in jsonResponse)
            {
                var email = user["email"]?.ToString();
                ClassicAssert.IsTrue(IsValidEmail(email), $"Invalid email format: {email}");
            }

            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Content: " + response.Content);
        }

        // Helper method to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }








        

    }
}
