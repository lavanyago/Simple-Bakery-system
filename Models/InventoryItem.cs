using System;
using System.Collections.Generic;

namespace Bakery.Models
{
    public class InventoryItem
    {
        public Guid Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public decimal Price {get;set;}
        public int DaysToSell {get;set;}
        public bool IsDeleted {get;set;}
        public List<Batch> Batches {get;set;}

        public Guid UserId { get; set; }
    }
}
