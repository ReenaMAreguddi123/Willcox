using System;
using System.Collections.Generic;
using Willcox.Contracts.Models;

namespace Willcox.Contracts
{
    public interface IDiscountService
    {
        /// <summary>
        /// Returns all active discount 
        /// </summary>
        /// <returns></returns>
        IList<PercentageDiscount> GetActiveDiscounts();
    }
}
