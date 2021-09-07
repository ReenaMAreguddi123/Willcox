using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Willcox.Contracts.Models
{
    /// <summary>
    /// Base class for the discounts
    /// </summary>
    public abstract class Discount
    {
        public string DiscountId { get; set; }
        public string Description { get; set; }
        //Priority of the discount - 1 being highest
        public int Priority { get; set; }
        //cannot be used with other discounts
        public bool Exclusive { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
