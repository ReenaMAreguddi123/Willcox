using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Willcox.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Percentage { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
