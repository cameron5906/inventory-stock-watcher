using System;
using System.Threading.Tasks;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Selectors;
using InventoryStockWatch.Core.Services;
using InventoryStockWatch.Core.Services.Scrapers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventoryStockWatch
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var productService = host.Services.GetRequiredService<ProductService>();

            var result = await productService.CheckProduct(new SourceDescriptor()
            {
                Url = "https://www.amazon.com/Nintendo-Switch-Neon-Blue-Joy%E2%80%91/dp/B07VGRJDFY/ref=sr_1_1?dchild=1&keywords=nintendo+switch&qid=1633722143&refinements=p_n_availability%3A2661601011&rnid=2661599011&sr=8-1",
                Type = SourceType.Webpage,
                PriceSelector = new HtmlPriceSelector()
                {
                    Selector = "span#price_inside_buybox"
                },
                StockSelector = new HtmlStockSelector()
                {
                    Selector = "body",
                    RegexTest = "^(?!.*(In stock soon|Out of stock).*$)"
                }
            });
            
            Console.WriteLine(result);
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<HtmlScraper, HtmlScraper>();
                    services.AddSingleton<JsonScraper, JsonScraper>();
                    services.AddSingleton<LinkedJsonScraper, LinkedJsonScraper>();
                    services.AddSingleton<ProductService, ProductService>();
                }).UseConsoleLifetime();
        }
    }
}