using System;
using System.Collections.Generic;
using OrderSystem.Core;
using OrderSystem.Infrastructure;

namespace OrderSystem.App {
    // [Architectural Smell: The God Object / High Efferent Coupling (CE)]
    // This class attempts to orchestrate UI, logic, infrastructure, and authentication.
    // It violates the "Four Elements of Simple Design" (High understandability, fewest components).
    public class GodProcessor {
        // [Complexity Smell: Accidental Complexity & Arrow Anti-Pattern]
        // Cyclomatic complexity explodes here, making the system highly rigid.
        public void ProcessSystem(List<Order> orders, User user) {
            AuthService auth = new AuthService();
            string hash = auth.HashPassword(user.PasswordHash);

            for (int i = 0; i < orders.Count; i++) {
                if (orders[i].Status == "New") {
                    // [Design Smell: Feature Envy & Inappropriate Intimacy]
                    // The processor intimately manipulates the internal data of the Order object.
                    if (orders[i].CustomerType == 1) {
                        // ----- START DRY VIOLATION 1 -----
                        // [Design Smell: Massive Code Duplication]
                        double tRate = 0.09;
                        double ship = 15.0;
                        if (orders[i].TotalAmount > 100) ship = 0;
                        orders[i].TotalAmount = orders[i].TotalAmount + (orders[i].TotalAmount * tRate) + ship;
                        // ----- END DRY VIOLATION 1 -----
                    } else if (orders[i].CustomerType == 2) {
                        // ----- START DRY VIOLATION 2 -----
                        double tRate = 0.09;
                        double ship = 15.0;
                        if (orders[i].TotalAmount > 100) ship = 0;
                        orders[i].TotalAmount = orders[i].TotalAmount + (orders[i].TotalAmount * tRate) + ship;
                        // ----- END DRY VIOLATION 2 -----

                        orders[i].TotalAmount -= 20;
                    } else if (orders[i].CustomerType == 3) {
                        // ----- START DRY VIOLATION 3 -----
                        double tRate = 0.09;
                        double ship = 15.0;
                        if (orders[i].TotalAmount > 100) ship = 0;
                        orders[i].TotalAmount = orders[i].TotalAmount + (orders[i].TotalAmount * tRate) + ship;
                        // ----- END DRY VIOLATION 3 -----

                        orders[i].TotalAmount -= 100;
                    }

                    // [Reliability Smell: Null Reference Risk]
                    if (orders[i].PaymentToken.Length > 5) {
                        Console.WriteLine("Token check passed.");
                    }

                    orders[i].Status = "Processed";
                    GlobalSystemCache.TotalProcessedOrders++;

                    // [Architectural Smell: Unbalanced Coupling (Distance vs Strength)]
                    // High Strength (direct instantiation) with High Distance (cross-layer call) creates a Distributed Monolith feel.
                    DataService dbService = new DataService();
                    dbService.SaveOrder(orders[i]);

                    // Triggering the massively duplicated invoice generator
                    InvoiceGenerator invoiceGen = new InvoiceGenerator();
                    if (orders[i].CustomerType == 1) invoiceGen.GenerateNormalInvoice(orders[i]);
                    else if (orders[i].CustomerType == 2) invoiceGen.GenerateVipInvoice(orders[i]);
                    else if (orders[i].CustomerType == 3) invoiceGen.GenerateCorporateInvoice(orders[i]);
                }
            }

            // [Maintainability Smell: Dead Code]
            // Variables assigned but never used.
            int obsoleteCalculation = 404;
        }
    }
}