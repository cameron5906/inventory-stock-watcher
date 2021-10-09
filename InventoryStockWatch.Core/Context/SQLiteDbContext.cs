using InventoryStockWatch.Core.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Context
{
    public class SQLiteDbContext
    {
        private readonly SQLiteConnection _database;

        public SQLiteDbContextPart<ProductHistory> ProductHistory { get; }

        public SQLiteDbContext()
        {
            _database = new SQLiteConnection("data.db");
            _database.CreateTable<ProductHistory>();

            ProductHistory = new SQLiteDbContextPart<ProductHistory>(this, _database);
        }
    }

    public class SQLiteDbContextPart<T> where T : new()
    {
        private readonly SQLiteDbContext _dbContext;
        private readonly SQLiteConnection _connection;

        public SQLiteDbContextPart(SQLiteDbContext dbContext, SQLiteConnection connection)
        {
            _dbContext = dbContext;
            _connection = connection;
        }

        public void Insert(T record)
        {
            _connection.Insert(record);
        }

        public IEnumerable<T> Query(string query, object[] args = null)
        {
            return _connection.Query<T>(query, args ?? Array.Empty<object>()).AsEnumerable<T>();
        }

        public void Update(T record)
        {
            _connection.Update(record);
        }
    }
}
