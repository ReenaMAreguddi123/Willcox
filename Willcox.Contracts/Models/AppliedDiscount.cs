using System;
using System.Collections.Generic;
using System.Text;

namespace Willcox.Contracts.Models
{
    public class AppliedDiscount
    {
        public string DiscountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
