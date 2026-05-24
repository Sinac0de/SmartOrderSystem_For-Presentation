using System;
using System.Collections.Generic;
using OrderSystem.Core;
using OrderSystem.Infrastructure;

namespace OrderSystem.App {
    // [Design Pattern Violation: The God Object / Blob Architecture]
    // A centralized controller component monopolizing processing responsibilities, orchestrating domain logic, 
    // managing validation flow, and directly calling out-of-process persistence side-effects.
    public class GodProcessor {
        // [Design Smell: Extreme Cognitive Complexity & Arrow Anti-Pattern]
        // [Design Smell: Long Method / Low Changeability]
        // Deeply nested procedural control flow branches exponentially increase cyclomatic weight,
        // making the architecture highly rigid and resistant to automated testing coverage frameworks.
        public void ProcessAllOrders(List<Order> orders) {
            for (int i = 0; i < orders.Count; i++) {
                if (orders[i].Status == "New") {
                    // [Design Smell: Feature Envy / Violation of Single Responsibility]
                    // This algorithm environment displays excessive intimacy with the internal state variables of Order.
                    // This data-manipulation framework logically belongs encapsulated inside the host model context.
                    if (orders[i].CustomerType == 1) {
                        if (orders[i].TotalAmount > 100) {
                            orders[i].TotalAmount = orders[i].TotalAmount - 10;
                        }
                    } else if (orders[i].CustomerType == 2) {
                        if (orders[i].TotalAmount > 50) {
                            orders[i].TotalAmount = orders[i].TotalAmount - (orders[i].TotalAmount * 0.2);
                        }

                        // [Design Smell: DRY (Don't Repeat Yourself) Duplication Smell]
                        // Duplicated mathematical calculation blocks inserted to emulate careless legacy velocity debt.
                        if (orders[i].TotalAmount > 500) {
                            orders[i].TotalAmount = orders[i].TotalAmount - 50;
                        }
                    } else if (orders[i].CustomerType == 3) {
                        if (orders[i].TotalAmount > 1000) {
                            orders[i].TotalAmount = orders[i].TotalAmount - 150;
                        } else {
                            orders[i].TotalAmount = orders[i].TotalAmount - 50;
                        }
                    }

                    orders[i].Status = "Processed";

                    // [Architecture Smell: Improper Coupling Balance & Tight Physical Integration Strength]
                    // Directly instantiating explicit infrastructure concrete service dependencies inside core logic paths.
                    // This structure actively violates the Dependency Inversion Principle (DIP).
                    DataService dbService = new DataService();
                    dbService.SaveOrderToDatabase(orders[i]);

                    Console.WriteLine("Order " + orders[i].OrderId + " processed.");
                }
            }

            // [Code Smell: Dead Code / Unused Local Allocation]
            // Declared memory reference assignment that is never evaluated. Checked and flagged during code quality analysis scans.
            int unusedVariableForSonarQubeMetric = 42;
        }
    }
}