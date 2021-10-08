using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsQuery;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Selectors;
using InventoryStockWatch.Core.Utils;

namespace InventoryStockWatch.Core.Services.Scrapers
{
    public class HtmlScraper : IScraper
    {
        public async Task<double?> GetPriceAsync(SourceDescriptor sourceDescriptor)
        {
            var htmlSelector = sourceDescriptor.PriceSelector as HtmlPriceSelector;
            
            using var client = new BrowserEmulator();
            var content = await client.GetAsync(sourceDescriptor.Url);
            var query = new CQ(content);
            var element = query.Find(htmlSelector.Selector);

            if (!element.Any())
                return null;

            var actionableText = string.IsNullOrEmpty(htmlSelector.Property)
                ? element.Text()
                : element.Attr(htmlSelector.Property);
            
            var priceString = Regex.Replace(actionableText, @"[^\d|\.]", "");
            
            if (!double.TryParse(priceString, out var price))
                return null;

            return price;
        }

        public async Task<bool> GetIsInStockAsync(SourceDescriptor sourceDescriptor)
        {
            var htmlSelector = sourceDescriptor.StockSelector as HtmlStockSelector;
            
            using var client = new BrowserEmulator();
            var content = await client.GetAsync(sourceDescriptor.Url);
            var query = new CQ(content);
            var element = query.Find(htmlSelector.Selector);

            if (!element.Any())
                return false;

            return Regex.IsMatch(element.Text(), htmlSelector.RegexTest);
        }
    }
}