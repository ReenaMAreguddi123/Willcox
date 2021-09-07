using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Willcox.Contracts;
using Willcox.Contracts.Models;

namespace Willcox.Services
{
    public class DiscountService : BaseService, IDiscountService
    {
        public DiscountService(ILogger<DiscountService> logger) : base(logger)
        {

        }

        /// <summary>
        /// Returns all active discounts.
        /// </summary>
        /// <returns></returns>
        public IList<PercentageDiscount> GetActiveDiscounts()
        {
            try
            {
                using (var r = new StreamReader($"{BinDir}/data/PercentageDiscounts.json"))
                {
                    var json = r.ReadToEnd();

                    var discounts = JsonConvert.DeserializeObject<IEnumerable<PercentageDiscount>>(json);

                    return (discounts ?? Array.Empty<PercentageDiscount>())
                        .Where(x => x.ValidFrom <= DateTime.Now && x.ValidTill >= DateTime.Now).ToList();
                }
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return Array.Empty<PercentageDiscount>();
            }
            
        }
    }
}
