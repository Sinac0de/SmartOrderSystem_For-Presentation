using OrderSystem.Core;

namespace OrderSystem.Infrastructure {
    // [Refactored: Dependency Inversion Principle]
    public interface IDataService {
        void SaveOrder(Order order);
    }

    public interface IAuthService {
        string HashPassword(string plainText);
    }
}