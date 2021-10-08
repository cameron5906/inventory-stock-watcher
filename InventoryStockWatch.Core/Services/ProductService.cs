using System.Threading.Tasks;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Services.Scrapers;

namespace InventoryStockWatch.Core.Services
{
    public class ProductService
    {
        private readonly HtmlScraper _htmlScraper;
        private readonly JsonScraper _jsonScraper;
        private readonly LinkedJsonScraper _linkedJsonScraper;

        public ProductService(HtmlScraper htmlScraper, JsonScraper jsonScraper, LinkedJsonScraper linkedJsonScraper)
        {
            _htmlScraper = htmlScraper;
            _jsonScraper = jsonScraper;
            _linkedJsonScraper = linkedJsonScraper;
        }
        
        public async Task<ProductCheckResult> CheckProduct(SourceDescriptor sourceDescriptor)
        {
            var stockResult = sourceDescriptor.StockSelector.Type switch
            {
                SelectorContentType.Json => await _jsonScraper.GetIsInStockAsync(sourceDescriptor),
                SelectorContentType.Html => await _htmlScraper.GetIsInStockAsync(sourceDescriptor),
                SelectorContentType.LinkedJson => await _linkedJsonScraper.GetIsInStockAsync(sourceDescriptor)
            };
            
            var priceResult = sourceDescriptor.PriceSelector.Type switch
            {
                SelectorContentType.Json => await _jsonScraper.GetPriceAsync(sourceDescriptor),
                SelectorContentType.Html => await _htmlScraper.GetPriceAsync(sourceDescriptor),
                SelectorContentType.LinkedJson => await _linkedJsonScraper.GetPriceAsync(sourceDescriptor)
            };
            
            return new ProductCheckResult
            {
                InStock = stockResult,
                Price = priceResult ?? -1
            };
        }
    }
}