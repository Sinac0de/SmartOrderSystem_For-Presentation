using System;
using OrderSystem.Core;

namespace OrderSystem.App {
    // [Design Smell: Missing Abstraction & Copy-Paste Programming]
    // Violates the DRY (Don't Repeat Yourself) principle. 
    // Fails the "Say everything once" rule from the Four Elements of Simple Design.
    public class InvoiceGenerator {
        // [Implementation Smell: Code Duplication]
        public void GenerateNormalInvoice(Order order) {
            string header = "====================================\n";
            header += $"INVOICE TYPE: NORMAL CUSTOMER\n";
            header += $"CUSTOMER NAME: {order.CustomerName}\n";
            header += $"ORDER REFERENCE: {order.OrderId}\n";
            header += $"TRANSACTION DATE: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n";
            header += "====================================\n";

            double baseTaxRate = 0.09;
            double shippingFee = 15.0;
            double handlingFee = 5.0;
            double processedAmount = order.TotalAmount + (order.TotalAmount * baseTaxRate);
            double finalTotal = processedAmount + shippingFee + handlingFee;

            string body = $"Original Amount: ${order.TotalAmount}\n";
            body += $"Calculated Tax (9%): ${order.TotalAmount * baseTaxRate}\n";
            body += $"Shipping Charges: ${shippingFee}\n";
            body += $"System Handling Fee: ${handlingFee}\n";
            body += "------------------------------------\n";
            body += $"TOTAL AMOUNT DUE: ${finalTotal}\n";

            string footer = "Thank you for your continued trust in our services.\n";
            footer += "If you have any questions regarding this invoice, please contact support.\n";
            footer += "End of official receipt.\n";

            Console.WriteLine(header + body + footer);
        }

        // ----- START MASSIVE DUPLICATION -----
        // [Implementation Smell: Shotgun Surgery Risk]
        // If the invoice layout changes, we must modify it in 3 different places.
        public void GenerateVipInvoice(Order order) {
            string header = "====================================\n";
            header += $"INVOICE TYPE: VIP CUSTOMER\n";
            header += $"CUSTOMER NAME: {order.CustomerName}\n";
            header += $"ORDER REFERENCE: {order.OrderId}\n";
            header += $"TRANSACTION DATE: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n";
            header += "====================================\n";

            double baseTaxRate = 0.09;
            double shippingFee = 0.0; // VIP gets free shipping
            double handlingFee = 5.0;
            double processedAmount = order.TotalAmount + (order.TotalAmount * baseTaxRate);
            double finalTotal = processedAmount + shippingFee + handlingFee;

            string body = $"Original Amount: ${order.TotalAmount}\n";
            body += $"Calculated Tax (9%): ${order.TotalAmount * baseTaxRate}\n";
            body += $"Shipping Charges: ${shippingFee}\n";
            body += $"System Handling Fee: ${handlingFee}\n";
            body += "------------------------------------\n";
            body += $"TOTAL AMOUNT DUE: ${finalTotal}\n";

            string footer = "Thank you for your continued trust in our services.\n";
            footer += "If you have any questions regarding this invoice, please contact support.\n";
            footer += "End of official receipt.\n";

            Console.WriteLine(header + body + footer);
        }

        // [Implementation Smell: Divergent Change]
        public void GenerateCorporateInvoice(Order order) {
            string header = "====================================\n";
            header += $"INVOICE TYPE: CORPORATE CUSTOMER\n";
            header += $"CUSTOMER NAME: {order.CustomerName}\n";
            header += $"ORDER REFERENCE: {order.OrderId}\n";
            header += $"TRANSACTION DATE: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n";
            header += "====================================\n";

            double baseTaxRate = 0.05; // Corporate gets lower tax
            double shippingFee = 15.0;
            double handlingFee = 5.0;
            double processedAmount = order.TotalAmount + (order.TotalAmount * baseTaxRate);
            double finalTotal = processedAmount + shippingFee + handlingFee;

            string body = $"Original Amount: ${order.TotalAmount}\n";
            body += $"Calculated Tax (5%): ${order.TotalAmount * baseTaxRate}\n";
            body += $"Shipping Charges: ${shippingFee}\n";
            body += $"System Handling Fee: ${handlingFee}\n";
            body += "------------------------------------\n";
            body += $"TOTAL AMOUNT DUE: ${finalTotal}\n";

            string footer = "Thank you for your continued trust in our services.\n";
            footer += "If you have any questions regarding this invoice, please contact support.\n";
            footer += "End of official receipt.\n";

            Console.WriteLine(header + body + footer);
        }
        // ----- END MASSIVE DUPLICATION -----
    }
}