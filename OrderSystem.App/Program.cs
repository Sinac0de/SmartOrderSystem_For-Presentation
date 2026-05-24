using System;
using System.Collections.Generic;
using OrderSystem.Core;

namespace OrderSystem.App {
    class Program {
        static void Main(string[] args) {
            // Initializing environment pipeline context with legacy mock data records
            var orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerName = "Sina Moradian", TotalAmount = 150, CustomerType = 1, Status = "New", CreatedDate = DateTime.Now },
                new Order { OrderId = 2, CustomerName = "Dr. Ashtiani", TotalAmount = 1200, CustomerType = 3, Status = "New", CreatedDate = DateTime.Now }
            };

            Console.WriteLine("=== Smart Order System Target Architecture ===");
            Console.WriteLine("[Warning] Executing highly coupled God Object iteration flow...");

            var processor = new GodProcessor();

            // Triggers pipeline execution loop sequence
            processor.ProcessAllOrders(orders);

            Console.WriteLine("Execution complete. Inspect static metrics dashboard for architectural debt detection analytics.");
        }
    }
}