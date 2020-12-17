using System;
using System.Linq;
using Bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery.DAL
{
    public class EFBatchStorage : IStoreBatches {
        InventoryContext _context;
        IStoreInventoryItems _itemStorage;

        public EFBatchStorage(InventoryContext context, IStoreInventoryItems itemStorage) {
            _context = context;
            _itemStorage = itemStorage;
        }

        public Batch GetBatch(Guid inventoryItemId, Guid batchId, Guid userid) {
            var allItems = _itemStorage.GetAll(userid);
            var item = allItems.FirstOrDefault(x => x.Id == inventoryItemId  &&  x.UserId == userid);
            var batch = item.Batches.FirstOrDefault(x => x.Id == batchId);
            return batch;
        }

        public void AddBatch(Batch batch, Guid userid ) {
            var item = _itemStorage.GetById(batch.InventoryItemId , userid);
            item.Batches.Add(batch);
            _context.SaveChanges();
        }

        public void UpdateBatch(Batch updatedBatch, Guid userid) {
            var item = _itemStorage.GetById(updatedBatch.InventoryItemId, userid);
            var batch = item.Batches.FirstOrDefault(x => x.Id == updatedBatch.Id);

            if (updatedBatch.RemainingQuantity > batch.RemainingQuantity) {
                throw new Exception("Invalid update");
            }

            batch.RemainingQuantity = updatedBatch.RemainingQuantity;
            _context.SaveChanges(); 
        }

        public void DeleteBatch(Guid Id, Guid batchId, Guid userid) {
            var item = _itemStorage.GetById(Id, userid);
            var batch = item.Batches.FirstOrDefault(x => x.Id == batchId);
            batch.IsArchived = true;
            _context.SaveChanges();
        }
    }
}
 