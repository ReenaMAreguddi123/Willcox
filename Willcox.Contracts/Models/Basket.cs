using System.Collections.Generic;
using System.Linq;

namespace Willcox.Contracts.Models
{
    public class Basket
    {
        public Basket()
        {
            Items = new List<BasketItem>();
        }

        public IList<BasketItem> Items { get; set; }

        /// <summary>
        /// Add item to the basket
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void Add(Product product, int quantity)
        {
            if (product == null || quantity <= 0)
                return;

            var basketItem = Items.FirstOrDefault(x => x.Product.ProductId == product.ProductId);
            if (basketItem != null)
            {
                basketItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new BasketItem() {Product = product, Quantity = quantity});
            }
        }

        /// <summary>
        /// Total basket price.
        /// </summary>
        public decimal Total
        {
            get { return Items.Sum(x => x.Quantity * x.Product.Price); }
        }
    }
}
