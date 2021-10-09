using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsQuery;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Selectors;
using InventoryStockWatch.Core.Models.Selectors.Price;
using InventoryStockWatch.Core.Models.Selectors.Stock;
using InventoryStockWatch.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventoryStockWatch.Core.Services.Scrapers
{
    public class JsonScraper : IScraper
    {
        public async Task<double?> GetPriceAsync(ProductSourceDescriptor sourceDescriptor)
        {
            var priceSelector = sourceDescriptor.PriceSelector as JsonPriceSelector;
            
            using var client = new BrowserEmulator();
            var content = await client.GetAsync(sourceDescriptor.Url);

            if (priceSelector.JsonContentType == JsonContentType.Array)
            {
                var jArray = JArray.Parse(content);

                return JsonUtil.GetValueFromArrayPath<double>(jArray, priceSelector.PathTemplate);
            } 
            else if (priceSelector.JsonContentType == JsonContentType.Object)
            {
                var jObject = JObject.Parse(content);

                return JsonUtil.GetValueFromObjectPath<double>(jObject, priceSelector.PathTemplate);
            }

            return null;
        }

        public async Task<bool> GetIsInStockAsync(ProductSourceDescriptor sourceDescriptor)
        {
            /*var stockSelector = sourceDescriptor.StockSelector as JsonStockSelector;
            
            using var client = new BrowserEmulator();
            var content = await client.GetAsync(sourceDescriptor.Url);

            if (stockSelector.JsonContentType == JsonContentType.Array)
            {
                var jArray = JArray.Parse(content);
                var value = 
            } 
            else if (stockSelector.JsonContentType == JsonContentType.Object)
            {
                var jObject = JObject.Parse(content);

                return JsonUtil.GetValueFromObjectPath<double>(jObject, priceSelector.PathTemplate);
            }*/

            return false;
        }
    }
}