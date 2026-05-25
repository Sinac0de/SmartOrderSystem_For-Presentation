using System;
using System.IO;
using Npgsql;
using OrderSystem.Core;

namespace OrderSystem.Infrastructure
{
    // [Architectural Smell: Mutable Global State / System Entropy]
    // Increases system entropy and makes side-by-side refactoring (Strangler Pattern) impossible.
    public static class GlobalSystemCache
    {
        public static int TotalProcessedOrders = 0;
    }

    public class DataService
    {
        // [Security Smell: Hardcoded Credentials in Source Code]
        // Essential complexity is ignored here, introducing severe security risks.
        public static string ConnectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Sina;";

        // [Architectural Smell: Mixing DDL with Persistence Logic]
        // Database schema creation should not reside in the runtime transaction flow.
        public void InitializeDirtyDatabase()
        {
            try 
            {
                using (var conn = new NpgsqlConnection(ConnectionString)) 
                {
                    conn.Open();
                    string createTableQuery = "CREATE TABLE IF NOT EXISTS Orders (Id INT, CustomerName VARCHAR(100), Amount FLOAT)";
                    using (var cmd = new NpgsqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            } 
            catch (Exception)
            {
                // [Implementation Smell: Exception Swallowing]
                // Error invisibility - violations are hidden from the monitoring system.
            }
        }

        public void SaveOrder(Order order)
        {
            try
            {
                // [Security Smell: SQL Injection Vulnerability]
                // Bypassing parameterized queries directly attacks the integrity of the data layer.
                string query = "INSERT INTO Orders (Id, CustomerName, Amount) VALUES (" + order.OrderId + ", '" + order.CustomerName + "', " + order.TotalAmount + ")";

                // [Reliability Smell: Resource Leakage]
                // NpgsqlConnection is left unmanaged. Exhausts the connection pool over time.
                NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                
                // [Reliability Smell: Unclosed IO Stream]
                StreamWriter writer = new StreamWriter("dummy_log.txt", true);
                writer.WriteLine("Order saved to PostgreSQL: " + order.OrderId);
            }
            catch (Exception)
            {
                // [Implementation Smell: Empty Catch Block]
            }
        }
    }
}