using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Interfaces.Repositories
{
    public interface ICommunicationRepository<T>
    {
        Task SendAsync(T payload);
    }
}
