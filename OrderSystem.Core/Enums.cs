namespace OrderSystem.Core {
    // [Refactored: Removed Magic Numbers]
    public enum CustomerCategory {
        Normal = 1,
        VIP = 2,
        Corporate = 3
    }

    public enum OrderStatus {
        New,
        Processed,
        Shipped
    }
}