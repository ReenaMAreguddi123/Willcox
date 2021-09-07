using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Willcox.Contracts;
using Willcox.Contracts.Models;
using Willcox.Services;

namespace Willcox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DI setup
            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error)
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<IDiscountService, DiscountService>()
                .AddTransient<IBasketService, BasketService>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting application");

            Basket(serviceProvider);

            logger.LogDebug("All done!");
        }

        private static void Basket(ServiceProvider serviceProvider)
        {
            //get active products
            var productService = serviceProvider.GetService<IProductService>();
            var products = productService.GetActiveProducts();

            var basket = new Basket();

            var basketOn = true;
            while (basketOn)
            {
                //display all products selection to user
                DisplayProductOptions(products);

                //selected product 
                var productId = Console.ReadLine();

                var selectedProduct = products.FirstOrDefault(x => x.ProductId.ToString() == productId);
                if (selectedProduct == null)
                {
                    Console.WriteLine("Sorry, not a valid selection");
                    DisplayProductOptions(products);
                }

                Console.Write("Enter the quantity:");
                //selected product quantity
                var quantity = Console.ReadLine();

                if (int.TryParse(quantity, out int selQuantity))
                {
                    basket.Add( selectedProduct, selQuantity);
                }

                Console.WriteLine("Add more items to basket? (Y or N)?");
                var moreItems = Console.ReadLine();

                if (moreItems?.ToUpper() == "N")
                {
                    basketOn = false;

                    //display basket summary in tabular data format
                    DisplayBasket(basket, serviceProvider);
                }
            }
        }

        /// <summary>
        /// Display products choice to user
        /// </summary>
        /// <param name="products"></param>
        private static void DisplayProductOptions(IEnumerable<Product> products)
        {
            var productOptions = new StringBuilder();
            productOptions.AppendLine("--- Products list ---");

            foreach (var product in products)
            {
                productOptions.AppendLine($"{product.ProductId} - {product.Name}");
            }

            Console.WriteLine(productOptions);

            Console.Write("Select the product:");
        }

        /// <summary>
        /// Display final basket summary in tabular format.
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="serviceProvider"></param>
        private static void DisplayBasket(Basket basket, ServiceProvider serviceProvider)
        {
            Console.Clear();

            Console.WriteLine("************************* Order Summary *************************");
            Console.WriteLine();

            DrawTable.PrintLine();
            DrawTable.PrintRow("Product", "Quantity", "Unit Price", "Total");
            DrawTable.PrintLine();

            foreach (var basketItem in basket.Items)
            {
                var totalPrice = basketItem.Quantity * basketItem.Product.Price;

                DrawTable.PrintRow(basketItem.Product.Name, basketItem.Quantity.ToString(),
                    $"£{basketItem.Product.Price}", $"£{totalPrice}");
            }

            DrawTable.PrintLine();

            var basketService = serviceProvider.GetService<IBasketService>();
            var summary = basketService.ApplyDiscount(basket);

            DrawTable.PrintRow("", "", "Subtotal", $"£{summary.Subtotal}");

            //Display all applied discounts to the basket
            foreach (var discount in summary.Discounts)
            {
                DrawTable.PrintRow("", "", $"{discount.Description}", $"-£{discount.Amount}");
            }

            DrawTable.PrintRow("", "", "Total", $"£{summary.Total}");

            DrawTable.PrintLine();
        }
    }
}
