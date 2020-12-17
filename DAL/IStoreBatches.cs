using System;
using System.Collections.Generic;
using System.Linq;
using Bakery.Models;

namespace Bakery.DAL
{
    public interface IStoreBatches {        
        Batch GetBatch(Guid InventoryItemId, Guid BatchId, Guid userid);
        void AddBatch(Batch newBatch,Guid userid);
        void UpdateBatch(Batch updatedBatch,Guid userid);
        void DeleteBatch(Guid id, Guid batchId, Guid userid);
    }
}
