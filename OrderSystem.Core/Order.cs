using System;

namespace OrderSystem.Core {
    // [Refactored: Rich Domain Model & Encapsulation]
    // Moved away from Anemic Domain Model. State is protected and behavior is inside the entity.
    public class Order {
        public int OrderId { get; private set; }
        public string CustomerName { get; private set; }
        public double TotalAmount { get; private set; }
        public CustomerCategory CustomerType { get; private set; }
        public OrderStatus Status { get; private set; }
        public string PaymentToken { get; private set; }

        public Order(int id, string customerName, double totalAmount, CustomerCategory type, string paymentToken) {
            OrderId = id;
            CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
            TotalAmount = totalAmount;
            CustomerType = type;
            PaymentToken = paymentToken;
            Status = OrderStatus.New;
        }

        public void UpdateTotalAmount(double newAmount) {
            if (newAmount < 0) throw new ArgumentException("Amount cannot be negative");
            TotalAmount = newAmount;
        }

        public void MarkAsProcessed() {
            Status = OrderStatus.Processed;
        }
    }
}