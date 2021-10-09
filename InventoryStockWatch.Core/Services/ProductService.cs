using System;
using System.Threading.Tasks;
using InventoryStockWatch.Core.Context;
using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Entities;
using InventoryStockWatch.Core.Repositories;
using InventoryStockWatch.Core.Services.Scrapers;

namespace InventoryStockWatch.Core.Services
{
    public class ProductService
    {
        private readonly HtmlScraper _htmlScraper;
        private readonly JsonScraper _jsonScraper;
        private readonly LinkedJsonScraper _linkedJsonScraper;
        private readonly ProductHistoryRepository _productHistoryRepository;

        public ProductService(HtmlScraper htmlScraper, JsonScraper jsonScraper, LinkedJsonScraper linkedJsonScraper,
            ProductHistoryRepository productHistoryRepository)
        {
            _htmlScraper = htmlScraper;
            _jsonScraper = jsonScraper;
            _linkedJsonScraper = linkedJsonScraper;
            _productHistoryRepository = productHistoryRepository;
        }
        
        public async Task<ProductCheckResult> CheckProductAsync(ProductSourceDescriptor sourceDescriptor)
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

            await _productHistoryRepository.AddHistoryItemAsync(new ProductHistory
            {
                Url = sourceDescriptor.Url,
                InStock = stockResult,
                Price = priceResult ?? -1,
                Title = sourceDescriptor.Title,
                LastCheckedAt = DateTimeOffset.UtcNow
            });
            
            return new ProductCheckResult
            {
                InStock = stockResult,
                Price = priceResult ?? -1
            };
        }
    }
}