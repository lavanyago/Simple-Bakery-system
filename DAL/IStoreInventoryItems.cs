using System;
using System.Collections.Generic;
using System.Linq;
using Bakery.Models;

namespace Bakery.DAL
{
    public interface IStoreInventoryItems {
        // Reads
        InventoryItem GetById(Guid id, Guid userid);
        List<InventoryItem> GetAll(Guid userid);

        // Updates
        void Create(InventoryItem item);
        void Update(InventoryItem updatedItem);
        void Delete(Guid id, Guid userid);
    }
}
