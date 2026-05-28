using Xunit;
using OrderSystem.Core;
using OrderSystem.App;
using System.Collections.Generic;

namespace OrderSystem.Tests {
    public class OrderProcessorTests {
        [Fact]
        public void ProcessOrders_ShouldCalculateCorrectTax_AndSave() {
            // Arrange
            var mockDb = new MockDataService();
            var processor = new CleanOrderProcessor(mockDb);
            var orders = new List<Order> {
                new Order(1, "Sina", 100, CustomerCategory.Normal, "T1"), // Normal: 100 + 9 + 0 = 109
                new Order(2, "Ashtiani", 100, CustomerCategory.VIP, "T2"), // VIP: 100 + 9 - 20 = 89
                new Order(3, "Corp", 100, CustomerCategory.Corporate, "T3") // Corp: 100 + 5 - 100 = 5
            };

            // Act
            processor.ProcessOrders(orders);

            // Assert
            Assert.Equal(109, orders[0].TotalAmount);
            Assert.Equal(89, orders[1].TotalAmount);
            Assert.Equal(5, orders[2].TotalAmount);
            Assert.Equal(OrderStatus.Processed, orders[0].Status);
        }

        [Fact]
        public void ProcessOrders_ShouldSkipAlreadyProcessed() {
            var mockDb = new MockDataService();
            var processor = new CleanOrderProcessor(mockDb);
            var order = new Order(1, "Sina", 50, CustomerCategory.Normal, "T1");
            order.MarkAsProcessed(); // Processed before

            processor.ProcessOrders(new List<Order> { order });

            Assert.Equal(OrderStatus.Processed, order.Status);
        }
    }
}