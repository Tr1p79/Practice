using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Examples
{
    internal class DatabaseOperations
    {
        private static string connectionString = "Data Source=(local);Initial Catalog=TestDB;Integrated Security=True";

        public static void PerformDatabaseOperations()
        {
            try
            {
                // Create a new database connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database connection opened successfully.");

                    // Create a table
                    string createTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
                    CREATE TABLE Employees (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(100),
                        Age INT
                    )";
                    ExecuteNonQuery(connection, createTableQuery);
                    Console.WriteLine("Table created successfully.");

                    // Insert data
                    string insertQuery = "INSERT INTO Employees (Name, Age) VALUES (@Name, @Age)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", "John Doe");
                        command.Parameters.AddWithValue("@Age", 30);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) inserted.");
                    }

                    // Read data
                    string selectQuery = "SELECT * FROM Employees";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("\nEmployees:");
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                int age = reader.GetInt32(2);
                                Console.WriteLine($"Id: {id}, Name: {name}, Age: {age}");
                            }
                        }
                    }

                    // Update data
                    string updateQuery = "UPDATE Employees SET Age = @Age WHERE Name = @Name";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Age", 31);
                        command.Parameters.AddWithValue("@Name", "John Doe");
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) updated.");
                    }

                    // Delete data
                    string deleteQuery = "DELETE FROM Employees WHERE Name = @Name";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", "John Doe");
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) deleted.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void ExecuteNonQuery(SqlConnection connection, string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
