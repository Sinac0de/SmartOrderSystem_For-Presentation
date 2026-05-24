using System;

namespace OrderSystem.Core {
    // [Design Smell: Anemic Domain Model & Primitive Obsession]
    // The class lacks behavior and relies entirely on primitive types. 
    // It is just a data clump violating the essential difficulty management.
    public class Order {
        public int OrderId;
        public string CustomerName;
        public double TotalAmount;
        public string CustomerAddress;

        // [Design Smell: Magic Numbers]
        // Conformity issue: 1 for Normal, 2 for VIP, 3 for Corporate. 
        // Reduces understandability.
        public int CustomerType;

        public string Status;
        public DateTime CreatedDate;
        public string PaymentToken;
    }

    public class User {
        public string Username;
        public string PasswordHash;
    }

    // [Abstraction Smell: Incomplete & Incorrect Abstraction]
    // Base class forces methods that not all subclasses can implement.
    public abstract class PaymentProcessor {
        public abstract void ProcessCreditCard();
        public abstract void ProcessCryptoWallet();
    }

    // [Design Smell: Refused Bequest & Liskov Substitution Principle Violation]
    // The subclass inherits methods it does not need and throws exceptions, breaking polymorphism.
    public class CashPayment : PaymentProcessor {
        public override void ProcessCreditCard() {
            throw new NotImplementedException("Cash payments do not use credit cards.");
        }

        public override void ProcessCryptoWallet() {
            throw new NotImplementedException("Cash payments do not use crypto.");
        }
    }
}