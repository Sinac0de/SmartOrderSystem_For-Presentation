using System;
using System.IO;
using Npgsql;
using OrderSystem.Core;

namespace OrderSystem.Infrastructure {
    public class DataService : IDataService {
        // Credentials should be injected via IConfiguration in a real app, 
        // but parameterized queries fix the critical vulnerability.
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Sina;";

        public void SaveOrder(Order order) {
            // [Refactored: Parameterized Queries (Fixes SQL Injection)]
            string query = "INSERT INTO Orders (Id, CustomerName, Amount) VALUES (@Id, @Name, @Amount)";

            // [Refactored: Proper Resource Management (Fixes Memory/Connection Leaks)]
            using (var connection = new NpgsqlConnection(_connectionString))
            using (var command = new NpgsqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Id", order.OrderId);
                command.Parameters.AddWithValue("@Name", order.CustomerName);
                command.Parameters.AddWithValue("@Amount", order.TotalAmount);

                try {
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Safe I/O operation
                    using (StreamWriter writer = new StreamWriter("dummy_log.txt", true)) {
                        writer.WriteLine($"Order saved securely: {order.OrderId}");
                    }
                } catch (NpgsqlException ex) {
                    // [Refactored: Proper Error Handling]
                    Console.WriteLine($"Database Error: {ex.Message}");
                    throw;
                }
            }
        }
    }
}