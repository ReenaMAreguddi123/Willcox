using System;
using System.Collections.Generic;
using System.Text;
using Willcox.Contracts.Models;

namespace Willcox.Contracts
{
    public interface IBasketService
    {
        /// <summary>
        /// Apply all eligible discounts to basket.
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        BasketSummary ApplyDiscount(Basket basket);
    }
}
