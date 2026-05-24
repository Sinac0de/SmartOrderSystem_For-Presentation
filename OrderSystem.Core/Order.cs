using System;

namespace OrderSystem.Core {
    // [Design Smell: Anemic Domain Model] 
    // This class exhibits an anemic structure, containing only raw data elements with zero localized behavior. 
    // It directly violates the fundamental object-oriented encapsulation boundary.
    // [Design Smell: Data Clumps & Primitive Obsession]
    // Consolidates a cluster of primitive types together without elevating them into domain-specific value objects.
    public class Order {
        public int OrderId;
        public string CustomerName;
        public double TotalAmount;

        // [Technical Debt: Magic Numbers / Low Understandability]
        // Representation of customer classification constants (1: Normal, 2: VIP, 3: Corporate).
        // This design pattern choice leaks internal structure and introduces significant fragility during modification.
        public int CustomerType;

        public string Status;
        public DateTime CreatedDate;
    }
}