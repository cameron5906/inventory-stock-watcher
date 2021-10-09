using System;
using System.Threading.Tasks;
using InventoryStockWatch.Core.Context;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Selectors;
using InventoryStockWatch.Core.Models.Selectors.Price;
using InventoryStockWatch.Core.Models.Selectors.Stock;
using InventoryStockWatch.Core.Repositories;
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
            var ingestService = host.Services.GetRequiredService<IngestService>();
            var productService = host.Services.GetRequiredService<ProductService>();

            var productDescriptors = await ingestService.GetAllProductsAsync();

            foreach(var productDescriptor in productDescriptors)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Checking {productDescriptor.Title}... ");
                var result = await productService.CheckProductAsync(productDescriptor);
                
                if(result.InStock)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"In Stock ({result.Price})");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Out of Stock");
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
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
                    services.AddSingleton<SQLiteDbContext, SQLiteDbContext>();
                    services.AddSingleton<IngestService, IngestService>();
                    services.AddSingleton<ProductHistoryRepository, ProductHistoryRepository>();
                }).UseConsoleLifetime();
        }
    }
}