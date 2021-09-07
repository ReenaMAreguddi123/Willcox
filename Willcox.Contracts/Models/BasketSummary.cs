using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Willcox.Contracts.Models
{
    public class BasketSummary
    {
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public IList<AppliedDiscount> Discounts { get; set; }
    }
}
