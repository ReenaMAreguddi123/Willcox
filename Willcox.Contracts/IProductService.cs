using System;
using System.Collections.Generic;
using Willcox.Contracts.Models;

namespace Willcox.Contracts
{
    public interface IProductService
    {
        /// <summary>
        ///Returns all active products
        /// </summary>
        /// <returns></returns>
        IList<Product> GetActiveProducts();
    }
}
