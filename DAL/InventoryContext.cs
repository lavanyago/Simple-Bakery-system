using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Bakery.Models;

namespace Bakery.DAL 
{
    public class InventoryContext : DbContext {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) {}

        public DbSet<InventoryItem> Items { get; set; }
    }
}