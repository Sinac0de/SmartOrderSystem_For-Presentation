using System;
using Microsoft.Data.SqlClient;
using OrderSystem.Core;

namespace OrderSystem.Infrastructure {
    public class DataService {
        // [Security Hotspot - Blocker Technical Debt] 
        // Hardcoded infrastructure secrets embedded inline. SonarQube flags this as a critical exposure asset threat.
        private static string ConnectionString = "Server=tcp:rentina-cluster.database.windows.net,1433;Initial Catalog=RentinaDB;User ID=admin;Password=SuperSecretPassword123!;TrustServerCertificate=True;";

        public void SaveOrderToDatabase(Order order) {
            try {
                // [Security Vulnerability - Critical: SQL Injection Vector]
                // Dynamic query creation via string concatenation directly breaks secure data layer patterns.
                // It introduces severe relational data vulnerability and bypasses parameterized design bounds.
                string query = "INSERT INTO Orders (Id, CustomerName, Amount) VALUES ("
                               + order.OrderId + ", '" + order.CustomerName + "', " + order.TotalAmount + ")";

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                // [Technical Debt - Major Smell: Blind Exception Swallowing / Empty Catch Block]
                // Silently consumes system fault exceptions without logging or propagation.
                // This mechanism completely obscures diagnostic visibility, leading to high system entropy.
            }
        }
    }
}