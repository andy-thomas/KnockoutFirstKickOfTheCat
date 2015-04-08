using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockoutFirstKickOfTheCat.Models;

namespace KnockoutFirstKickOfTheCat.Controllers
{ 
    public class ProductController : Controller
    {
        private StoreContext db = new StoreContext();
        string Result = string.Empty;

        //
        // GET: /Product/

        public ViewResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            ViewBag.CategoryList = db.Categories;
            return View(products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ViewResult Details(int id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        } 

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
        
        //
        // GET: /Product/Edit/5
 
        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        //
        // GET: /Product/Delete/5
 
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // POST: /Product/SaveForm
 
        [HttpPost]
        public ActionResult SaveForm(IEnumerable<Product> products) // see note 1
        {

            
            //if (ModelState.IsValid)
            //{
            ////    db.Entry(product).State = EntityState.Modified;
            ////    db.SaveChanges();
            ////    return RedirectToAction("Index");
            //}
            // Delete the items marked as inactive

            if (products != null)
            {
                products.Where(p => !p.IsActive && p.Id > 0).ToList().ForEach(DeleteProduct);

                // Update the active dirty items
                products.Where(p => p.IsActive && p.Id > 0 && p.IsDirty).ToList().ForEach(UpdateProduct);

                // Add the active items with id = 0
                products.Where(p => p.IsActive && p.Id == 0).ToList().ForEach(InsertProduct);
            }

            return RedirectToAction("Index");
        }

        
        private void DeleteProduct(Product product)
        {
            Product repoProduct = db.Products.Find(product.Id);
            db.Products.Remove(repoProduct);
            db.SaveChanges();       
        }

        private void UpdateProduct(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void InsertProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

    }
    
}

// Note 1: If using the form with the hidden field to port the data, 
// then use the [FromJson] attribute 
// or the JsonModelBinder (and register it in global.asax.
// If using the viewmodel save method, do not use it.