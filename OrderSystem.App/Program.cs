using System;
using System.Collections.Generic;
using OrderSystem.Core;
using OrderSystem.Infrastructure;

namespace OrderSystem.App {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("=== Starting Toxic Architecture System ===");

            var dbSetup = new DataService();
            dbSetup.InitializeDirtyDatabase();
            Console.WriteLine("PostgreSQL connected (Dirty DDL Executed).");

            var user = new User { Username = "admin", PasswordHash = "123456" };

            // Purposefully leaving PaymentToken as null to simulate runtime risk
            var orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerName = "Sina Moradian", TotalAmount = 50, CustomerType = 1, Status = "New", PaymentToken = "TOKEN123" },
                new Order { OrderId = 2, CustomerName = "Dr. Ashtiani", TotalAmount = 500, CustomerType = 2, Status = "New", PaymentToken = "TOKEN456" }
            };

            Console.WriteLine("Executing God Object...");
            var processor = new GodProcessor();

            try {
                processor.ProcessSystem(orders, user);
            } catch (Exception ex) {
                Console.WriteLine($"[Expected Runtime Failure Captured]: {ex.Message}");
            }

            Console.WriteLine($"Total processed in Global Cache: {GlobalSystemCache.TotalProcessedOrders}");
            Console.WriteLine("Execution finished! Check pgAdmin for records and SonarQube for debt metrics.");
        }
    }
}