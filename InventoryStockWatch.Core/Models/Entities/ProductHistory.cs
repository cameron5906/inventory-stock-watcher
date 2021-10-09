using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Models.Entities
{
    public class ProductHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public bool InStock { get; set; }

        public double Price { get; set; }

        public DateTimeOffset LastCheckedAt { get; set; }
    }
}
