using System;
using System.Collections.Generic;
using OrderSystem.Core;
using OrderSystem.Infrastructure;

namespace OrderSystem.App {
    // [Refactored: Single Responsibility Principle & Loose Coupling]
    public class CleanOrderProcessor {
        private readonly IDataService _dataService;

        // Dependency Injection reduces 'Integration Strength' (Balanced Coupling)
        public CleanOrderProcessor(IDataService dataService) {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        public void ProcessOrders(IEnumerable<Order> orders) {
            foreach (var order in orders) {
                if (order.Status != OrderStatus.New) continue;

                // [Refactored: Resolving Essential Complexity cleanly]
                ITaxCalculationStrategy taxStrategy = GetTaxStrategy(order.CustomerType);
                double finalAmount = taxStrategy.CalculateFinalAmount(order.TotalAmount);

                order.UpdateTotalAmount(finalAmount);
                order.MarkAsProcessed();

                _dataService.SaveOrder(order);
                Console.WriteLine($"Order {order.OrderId} processed successfully via Strategy.");
            }
        }

        private ITaxCalculationStrategy GetTaxStrategy(CustomerCategory category) {
            return category switch {
                CustomerCategory.VIP => new VipTaxStrategy(),
                CustomerCategory.Corporate => new CorporateTaxStrategy(),
                _ => new NormalTaxStrategy()
            };
        }
    }
}