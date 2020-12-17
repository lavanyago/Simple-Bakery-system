using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bakery.Models;
using Bakery.DAL;

namespace Bakery.Controllers
{
    public class InventoryController : Controller
    {
        private IStoreInventoryItems _inventoryStorage;

        public InventoryController(IStoreInventoryItems inventoryStorage)
        {
            _inventoryStorage = inventoryStorage;
        }

        public IActionResult Index(Guid userid) {
            var result = _inventoryStorage.GetAll(userid);
            return View(result);
        }

        public IActionResult Details(Guid id , Guid userid) {
            var item = _inventoryStorage.GetById(id , userid);
            return View(item);
        }

        public IActionResult Create() {
            return View("Upsert");
        }

        [HttpPost]
        public IActionResult Create(InventoryItem newItem) {
            newItem.Id = Guid.NewGuid();
            newItem.Batches = new List<Batch>();
            _inventoryStorage.Create(newItem);
            return RedirectToAction("Index");
        }

        public IActionResult Update(Guid id, Guid userid ) {
            ViewBag.IsEditing = true;
            var item = _inventoryStorage.GetById(id , userid);
            return View("Upsert", item);
        }

        [HttpPost]
        public IActionResult Update(InventoryItem updatedItem) {
            _inventoryStorage.Update(updatedItem);
            return RedirectToAction("Details", "Inventory", new { id = updatedItem.Id});
        }

        [HttpPost]
        public IActionResult Delete(Guid id , Guid userid) {
            _inventoryStorage.Delete(id , userid);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
