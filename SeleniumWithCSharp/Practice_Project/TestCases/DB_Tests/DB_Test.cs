using MySql.Data.MySqlClient;
using Practice_Project.Utilities.Database_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Project.TestCases.DB_Tests
{
    public class DB_Test
    {
        [Test]  // Fetch All Employee Details and print in console
        public void Fetch_Emp_Details_01()
        {
            MySqlConnection connection = Emp_DB.ConnectDB();  // Establish the connection
            if (connection != null)   // Create a Statement object to execute queries
            {
                // Ensure that CommandText is properly initialized with SQL query
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM employees";  // Initialize CommandText with SQL query
                MySqlDataReader reader = cmd.ExecuteReader();  // Execute the query
                while (reader.Read())  // Process the result
                {
                    // Retrieve by column name
                    int employeeId = reader.GetInt32("employee_id");
                    string firstName = reader.GetString("first_name");
                    string lastName = reader.GetString("last_name");
                    string position = reader.GetString("position");
                    double salary = reader.GetDouble("salary");
                    string hireDate = reader.GetDateTime("hire_date").ToString("yyyy-MM-dd");

                    // Print employee details
                    TestContext.Out.WriteLine($"Employee ID: {employeeId}");
                    TestContext.Out.WriteLine($"First Name: {firstName}");
                    TestContext.Out.WriteLine($"Last Name: {lastName}");
                    TestContext.Out.WriteLine($"Position: {position}");
                    TestContext.Out.WriteLine($"Salary: {salary}");
                    TestContext.Out.WriteLine($"Hire Date: {hireDate}");
                    TestContext.Out.WriteLine("------------");
                }
                reader.Close();    // Close the connection
                connection.Close();
            }
        }









    }
}
