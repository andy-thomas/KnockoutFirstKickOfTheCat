using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockoutFirstKickOfTheCat.Models;

namespace KnockoutFirstKickOfTheCat.Controllers
{
    public class ProductSRController : Controller
    {
        //
        // GET: /ProductSR/

        public ActionResult Index()
        {
            using (var context = new StoreContext())
            {
                try
                {
                    var result = context.Categories.ToArray();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return View();
        }

    }
}
