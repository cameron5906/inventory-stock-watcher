using InventoryStockWatch.Core.Context;
using InventoryStockWatch.Core.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Repositories
{
    public class ProductHistoryRepository
    {
        private readonly SQLiteDbContext _dbContext;

        public ProductHistoryRepository(SQLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddHistoryItemAsync(ProductHistory productHistory)
        {
            await Task.CompletedTask;
            _dbContext.ProductHistory.Insert(productHistory);
        }

        public async Task<bool> CheckProductExistsAsync(string productUrl)
        {
            await Task.CompletedTask;
            return _dbContext.ProductHistory.Query("select * from ProductHistory where Url = ?", new[] { productUrl }).Any();
        }

        public async Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(string productUrl)
        {
            await Task.CompletedTask;
            return _dbContext.ProductHistory.Query("select * from ProductHistory order by CheckedAt desc");
        }
    }
}
