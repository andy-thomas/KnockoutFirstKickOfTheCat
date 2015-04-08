using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutFirstKickOfTheCat.Models;
using SignalR.Hubs;

namespace KnockoutFirstKickOfTheCat.SignalRHubs
{
    public class ProductHub : Hub
    {
        /// <summary>
        /// Get all products using SignalR
        /// </summary>
        public void GetAllProducts()
        {
            using (var context = new StoreContext())
            {
                var result = context.Products.ToArray();
                Caller.renderAllProducts(result);
            }
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        public void GetAllCategories()
        {
            using (var context = new StoreContext())
            {
                var result = context.Categories.ToArray();
                Caller.renderAllCategories(result);
            }
        }

        public bool Update(Product product)
        {
            using (var context = new StoreContext())
            {
                var oldProduct = context.Products.FirstOrDefault(p => p.Id == product.Id);

                try
                {
                    if (oldProduct == null || oldProduct.Id == 0)
                        return false;
                    else
                    {
                        oldProduct.Name = product.Name;
                        oldProduct.Description = product.Description;
                        oldProduct.CategoryId = product.CategoryId;
                        oldProduct.ActiveDate = product.ActiveDate;
                        oldProduct.UnitPrice = product.UnitPrice;
                        oldProduct.IsActive = product.IsActive;

                        context.SaveChanges();
                        Clients.productUpdated(oldProduct);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Caller.reportError("Oops! Unable to update product: " + ex);
                    return false;
                }
            }
        }

        public bool Remove(int id)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    Product product = context.Products.Find(id);
                    context.Products.Remove(product);
                    context.SaveChanges();
                    Clients.productRemoved(id);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Caller.reportError("Oops! Unable to remove product: " + ex);
                return false;
            }
        }

        public bool Add(Product product)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    Product savedProduct = context.Products.Add(product);
                    context.SaveChanges();
                    Clients.productAdded(savedProduct);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Caller.reportError("Oops! Unable to add product: " + ex);
                return false;
            }
        }

    }
}