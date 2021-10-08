using System.Threading.Tasks;
using InventoryStockWatch.Core.Models;

namespace InventoryStockWatch.Core.Services.Scrapers
{
    public interface IScraper
    {
        Task<double?> GetPriceAsync(SourceDescriptor sourceDescriptor);
        Task<bool> GetIsInStockAsync(SourceDescriptor sourceDescriptor);
    }
}