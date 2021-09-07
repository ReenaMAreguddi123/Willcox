using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Willcox.Contracts;
using Willcox.Contracts.Models;

namespace Willcox.Services
{
    public class ProductService : BaseService, IProductService
    {
        private IList<Product> _products;

        public ProductService(ILogger<ProductService> logger) : base(logger)
        {

        }

        /// <summary>
        /// Returns all active products.
        /// </summary>
        /// <returns></returns>
        public IList<Product> GetActiveProducts()
        {
            Logger.LogInformation("Get active products");

            try
            {
                if (_products != null)
                    return _products;

                using (var r = new StreamReader($"{BinDir}/data/products.json"))
                {
                    var json = r.ReadToEnd();
                    var products = JsonConvert.DeserializeObject<IList<Product>>(json);
                    _products = products;

                    return _products;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Array.Empty<Product>();
            }            
        }
    }
}
