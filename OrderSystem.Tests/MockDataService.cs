using OrderSystem.Core;
using OrderSystem.Infrastructure;

namespace OrderSystem.Tests {
    //  This mock will simulate the behavior of the IDataService without performing any actual data operations.
    public class MockDataService : IDataService {
        public void SaveOrder(Order order) {
            // Nothing ;)
        }
    }
}