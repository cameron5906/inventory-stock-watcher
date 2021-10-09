using System;
using System.Threading.Tasks;
using InventoryStockWatch.Core.Context;
using InventoryStockWatch.Core.Interfaces.Repositories;
using InventoryStockWatch.Core.Interfaces.Services;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Communication;
using InventoryStockWatch.Core.Repositories;
using InventoryStockWatch.Core.Repositories.Communication;
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
            var communicationService = host.Services.GetRequiredService<CommunicationService>();
            var configurationService = host.Services.GetRequiredService<IConfigurationService>();

            var productDescriptors = await ingestService.GetAllProductsAsync();

            foreach(var productDescriptor in productDescriptors)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Checking {productDescriptor.Title}... ");

                var result = await productService.CheckProductAsync(productDescriptor);
                var lastResult = await productService.GetLastProductCheckAsync(productDescriptor);
                
                if(result.InStock)
                {
                    if(lastResult is null || !lastResult.InStock)
                    {
                        await communicationService.SendMessageAsync(configurationService.GetAlertRecipient(), $"{productDescriptor.Title} is now in stock for ${result.Price}!", "Product now in stock");
                    }

                    if(lastResult is not null && lastResult.Price > result.Price)
                    {
                        await communicationService.SendMessageAsync(configurationService.GetAlertRecipient(), $"Price has dropped for {productDescriptor.Title} from ${lastResult.Price} to {result.Price}!", "Product price drop");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"In Stock ({result.Price})");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Out of Stock");
                }

                await productService.SaveHistoryAsync(productDescriptor, result);
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
                    services.AddSingleton<ICommunicationRepository<SMSMessage>, TwilioCommunicationRepository>();
                    services.AddSingleton<ICommunicationRepository<Email>, EmailCommunicationRepository>();
                    services.AddSingleton<CommunicationService, CommunicationService>();
                    services.AddSingleton<IConfigurationService, EnvironmentConfigurationService>();
                }).UseConsoleLifetime();
        }
    }
}