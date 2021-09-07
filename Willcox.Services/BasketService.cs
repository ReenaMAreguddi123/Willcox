using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Willcox.Contracts;
using Willcox.Contracts.Models;

namespace Willcox.Services
{
    public class BasketService : BaseService, IBasketService
    {
        private readonly IDiscountService _discountService;

        public BasketService(IDiscountService discountService, ILogger<BasketService> logger) : base(logger)
        {
            _discountService = discountService;
        }

        /// <summary>
        ///  Apply all eligible discounts to basket.
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>

        public BasketSummary ApplyDiscount(Basket basket)
        {
            var summary = new BasketSummary()
                {Subtotal = basket.Total, Total = basket.Total, Discounts = new List<AppliedDiscount>()};

            try
            {
                //get active discounts
                var discounts = _discountService.GetActiveDiscounts();

                if (!discounts.Any())
                    return summary;

                //loop through each basket line item
                foreach (var lineItem in basket.Items)
                {
                    //get valid discounts for this product
                    var validDiscounts = discounts.Where(x => x.Products.Contains(lineItem.Product.ProductId))
                        .OrderBy(x => x.Priority).ToList();

                    if (!validDiscounts.Any())
                        continue;

                    //loop through all valid discounts
                    foreach (var percentageDiscount in validDiscounts)
                    {
                        //calculate discount amount
                        var discountAmount = lineItem.Quantity *
                                             (percentageDiscount.PercentageOff * lineItem.Product.Price / 100);

                        summary.Total -= discountAmount;

                        //check if this discount is already applied 
                        var appliedDiscount = summary.Discounts.FirstOrDefault(x => x.DiscountId == percentageDiscount.DiscountId);
                        if (appliedDiscount == null)
                        {
                            //add to applied discounts list
                            summary.Discounts.Add(new AppliedDiscount()
                            {
                                DiscountId = percentageDiscount.DiscountId,
                                Description = percentageDiscount.Description,
                                Amount = discountAmount
                            });
                        }
                        else
                        {
                            // Increase applied discount
                            appliedDiscount.Amount += discountAmount;
                        }

                        //is it exclusive discount?? then exit
                        if (percentageDiscount.Exclusive)
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.LogError("Failed to apply discount", ex);
            }

            return summary;
        }
    }
}
