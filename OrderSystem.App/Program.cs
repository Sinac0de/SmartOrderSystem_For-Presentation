using System;
using System.Collections.Generic;
using OrderSystem.Core;
using OrderSystem.Infrastructure;

namespace OrderSystem.App {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("=== Executing Clean Architecture System ===");

            var orders = new List<Order>
            {
                new Order(1, "Sina", 50, CustomerCategory.Normal, "TOK_123"),
                new Order(2, "Dr. Ashtiani", 500, CustomerCategory.VIP, "TOK_456")
            };

            // [Refactored: Dependency Injection wire-up]
            IDataService dataService = new DataService();
            var processor = new CleanOrderProcessor(dataService);

            processor.ProcessOrders(orders);

            Console.WriteLine("Execution finished! Check SonarQube for A-Grade metrics!");
        }
    }
}