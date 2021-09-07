using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Willcox.Contracts.Models
{
    /// <summary>
    /// Percentage discount type
    /// </summary>
    public class PercentageDiscount : Discount
    {
        public IList<string> Products { get; set; }
        public decimal PercentageOff { get; set; }
    }
}
