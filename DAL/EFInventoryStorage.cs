using System;
using System.Collections.Generic;
using System.Linq;
using Bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery.DAL
{
    public class EFInventoryStorage : IStoreInventoryItems {
        InventoryContext _context;

        public EFInventoryStorage(InventoryContext context) {
            _context = context;
        }

        public InventoryItem GetById(Guid id, Guid userid) {
            return _context.Items.Include(x => x.Batches).FirstOrDefault(x => x.Id == id && x.IsDeleted == false && x.UserId == userid);
        }
        public List<InventoryItem> GetAll(Guid userid) {
            return _context.Items
                .Include(x => x.Batches)
                .Where(x => x.IsDeleted == false &&  x.UserId == userid)
                .ToList();
        }


        public void Create(InventoryItem item) {
            _context.Add(item);
            _context.SaveChanges();
        }

        public void Update(InventoryItem updatedItem) {
            _context.Update(updatedItem);
            _context.SaveChanges();
        }

        public void Delete(Guid id , Guid userid) {
            var item = GetById(id, userid);
            item.IsDeleted = true;
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
 