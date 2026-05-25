using OrderSystem.Core;

namespace OrderSystem.App {
    // [Refactored: Strategy Pattern & Open/Closed Principle]
    // Replaces the massive duplication and Arrow Anti-Pattern.
    public interface ITaxCalculationStrategy {
        double CalculateFinalAmount(double baseAmount);
    }

    public class NormalTaxStrategy : ITaxCalculationStrategy {
        public double CalculateFinalAmount(double baseAmount) {
            return baseAmount + (baseAmount * 0.09) + (baseAmount > 100 ? 0 : 15.0);
        }
    }

    public class VipTaxStrategy : ITaxCalculationStrategy {
        public double CalculateFinalAmount(double baseAmount) {
            double amountWithTax = baseAmount + (baseAmount * 0.09); // Free shipping
            return amountWithTax - 20; // VIP discount
        }
    }

    public class CorporateTaxStrategy : ITaxCalculationStrategy {
        public double CalculateFinalAmount(double baseAmount) {
            double amountWithTax = baseAmount + (baseAmount * 0.05); // Lower tax
            return amountWithTax - 100; // Corporate discount
        }
    }
}