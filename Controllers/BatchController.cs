using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bakery.Models;
using Bakery.DAL;

namespace Bakery.Controllers
{
    public class BatchController : Controller
    {
        private readonly ILogger<BatchController> _logger;
        private IStoreBatches _batchStorage;

        public BatchController(ILogger<BatchController> logger, IStoreBatches batchStorage)
        {
            _logger = logger;
            _batchStorage = batchStorage;
        }

        public IActionResult Create(Guid inventoryItemId)
        {
            var batch = new Batch();
            batch.InventoryItemId = inventoryItemId;
            return View(batch);
        }

        [HttpPost]
        public IActionResult Create(Batch newBatch , Guid userid)
        {
            newBatch.RemainingQuantity = newBatch.Quantity;
            _batchStorage.AddBatch(newBatch, userid);
            return RedirectToAction("Details", "Inventory", new { id = newBatch.InventoryItemId});
        }

        public IActionResult Update(Guid inventoryItemId, Guid batchId , Guid userid)
        {
            var batch = _batchStorage.GetBatch(inventoryItemId, batchId , userid);
            return View(batch);
        }

        [HttpPost]
        public IActionResult Update(Batch updatedBatch , Guid userid)
        {
            _batchStorage.UpdateBatch(updatedBatch , userid);
            return RedirectToAction("Details", "Inventory", new { id = updatedBatch.InventoryItemId});
        }

        [HttpPost]
        public IActionResult Delete(Guid inventoryItemId, Guid Id, Guid userid) {
            _batchStorage.DeleteBatch(inventoryItemId, Id, userid);
            return RedirectToAction("Details", "Inventory", new { id = inventoryItemId});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
