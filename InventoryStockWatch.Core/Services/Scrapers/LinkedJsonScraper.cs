using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsQuery;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Selectors;
using InventoryStockWatch.Core.Models.Selectors.Price;
using InventoryStockWatch.Core.Models.Selectors.Stock;
using InventoryStockWatch.Core.Utils;
using Newtonsoft.Json.Linq;

namespace InventoryStockWatch.Core.Services.Scrapers
{
    public class LinkedJsonScraper : IScraper
    {
        public async Task<double?> GetPriceAsync(ProductSourceDescriptor sourceDescriptor)
        {
            var priceSelector = sourceDescriptor.PriceSelector as LinkedJsonPriceSelector;
            
            using var client = new BrowserEmulator();
            var content = await client.GetAsync(sourceDescriptor.Url);
            var query = new CQ(content);
            var element = query.Find("script[type='application/ld+json']");

            if (!element.Any())
                throw new DataMisalignedException("Incorrect price selector");

            var json = element.First().Text();
            
            var jObject = JObject.Parse(json);

            return JsonUtil.GetValueFromObjectPath<double>(jObject, priceSelector.PathTemplate);
        }

        public async Task<bool> GetIsInStockAsync(ProductSourceDescriptor sourceDescriptor)
        {
            var stockSelector = sourceDescriptor.StockSelector as LinkedJsonStockSelector;
            
            using var client = new BrowserEmulator();
            var content = await client.GetAsync(sourceDescriptor.Url);
            var query = new CQ(content);
            var element = query.Find("script[type='application/ld+json']");

            if (!element.Any())
                throw new DataMisalignedException("Incorrect stock selector");

            var json = element.First().Text();
            
            var jObject = JObject.Parse(json);
            var value = JsonUtil.GetValueFromObjectPath<string>(jObject, stockSelector.PathTemplate);
            return Regex.IsMatch(value, stockSelector.RegexTest);
        }
    }
}