using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Project.Utilities.Database_Manager
{
    public class Emp_DB
    {
        public static MySqlConnection ConnectDB()  // Method to establish a connection to the MySQL database
        {
            try
            {
                // Define the MySQL connection string
                // The string contains server address, database name, username, and password
                string connectionString = "Server=localhost; Database=employee_db; Uid=root; Pwd=root;";

                // Create and return a MySqlConnection object with the connection string
                MySqlConnection connection = new (connectionString);

                connection.Open();   // Open the connection to the database
                TestContext.Out.WriteLine("Connected to the database!");
                return connection;  // Return the established connection
            }
            catch (Exception e)
            {
                TestContext.Out.WriteLine(e.Message);
                return null;  // Return null if connection fails
            }
        }




        // Method to fetch employee data from the 'employees' table in the database
        public static List<string> FetchEmployeeData()
        {
            List<string> employees = new List<string>();  // List to hold employee data strings
            MySqlConnection connection = ConnectDB();  // Get the database connection by calling ConnectDB method

            if (connection != null)   // Check if connection is valid
            {
                // Create a MySqlCommand object to execute the SQL query
                // The query will fetch all records from the employees table
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM employees", connection);

                // Execute the query and get a MySqlDataReader to read the results
                MySqlDataReader reader = cmd.ExecuteReader();

                // Read through the result set and process each row
                while (reader.Read())
                {
                    // Build a string that combines employee data from each row
                    string employeeDetails = "Employee ID: " + reader.GetInt32("employee_id") +
                                             ", Name: " + reader.GetString("first_name") + " " + reader.GetString("last_name") +
                                             ", Position: " + reader.GetString("position") +
                                             ", Salary: " + reader.GetDecimal("salary") +
                                             ", Hire Date: " + reader.GetString("hire_date");

                    // Add the formatted employee data string to the list
                    employees.Add(employeeDetails);
                }

                
                reader.Close();  // Close the data reader once we're done
                cmd.Dispose();   // Close the MySQL command object
                connection.Close();  // Close the database connection
            }
            return employees;  // Return the list of employee data strings
        }






    }
}
