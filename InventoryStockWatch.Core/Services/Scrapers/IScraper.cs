using System.Threading.Tasks;
using InventoryStockWatch.Core.Models;

namespace InventoryStockWatch.Core.Services.Scrapers
{
    public interface IScraper
    {
        Task<double?> GetPriceAsync(ProductSourceDescriptor sourceDescriptor);
        Task<bool> GetIsInStockAsync(ProductSourceDescriptor sourceDescriptor);
    }
}