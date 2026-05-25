using System;

namespace OrderSystem.Core {
    // [Refactored: Interface Segregation Principle & Liskov Substitution]
    // Separated the abstractions so classes only inherit what they actually use.
    public interface IPaymentProcessor {
        void ProcessPayment(Order order);
    }

    public interface IDigitalPaymentProcessor : IPaymentProcessor {
        void ProcessCryptoWallet(Order order);
    }

    public class CashPayment : IPaymentProcessor {
        public void ProcessPayment(Order order) {
            Console.WriteLine($"Processing physical cash payment for {order.CustomerName}.");
        }
    }
}