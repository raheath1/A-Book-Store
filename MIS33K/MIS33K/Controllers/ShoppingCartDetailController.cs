using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS33K.Models;

namespace MIS33K.Controllers
{
    public class ShoppingCartDetailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /ShoppingCartDetail/
        public async Task<ActionResult> Index()
        {
            var shoppingcartdetails = db.ShoppingCartDetails.Include(s => s.Book);
            return View(await shoppingcartdetails.ToListAsync());
        }

        // GET: /ShoppingCartDetail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartDetail shoppingcartdetail = await db.ShoppingCartDetails.FindAsync(id);
            if (shoppingcartdetail == null)
            {
                return HttpNotFound();
            }
            return View(shoppingcartdetail);
        }

        //// GET: /ShoppingCartDetail/Create
        //public ActionResult Create()
        //{
        //    ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
        //    return View();
        //}

        //// POST: /ShoppingCartDetail/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include="ShoppingCartDetailID,BookID,Quantity,Title,CustomerPrice,PriceLastPaid")] ShoppingCartDetail shoppingcartdetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ShoppingCartDetails.Add(shoppingcartdetail);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", shoppingcartdetail.BookID);
        //    return View(shoppingcartdetail);
        //}

        //// GET: /ShoppingCartDetail/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ShoppingCartDetail shoppingcartdetail = await db.ShoppingCartDetails.FindAsync(id);
        //    if (shoppingcartdetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", shoppingcartdetail.BookID);
        //    return View(shoppingcartdetail);
        //}

        //// POST: /ShoppingCartDetail/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include="ShoppingCartDetailID,BookID,Quantity,Title,CustomerPrice,PriceLastPaid")] ShoppingCartDetail shoppingcartdetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(shoppingcartdetail).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", shoppingcartdetail.BookID);
        //    return View(shoppingcartdetail);
        //}

        // GET: /ShoppingCartDetail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartDetail shoppingcartdetail = await db.ShoppingCartDetails.FindAsync(id);
            if (shoppingcartdetail == null)
            {
                return HttpNotFound();
            }
            return View(shoppingcartdetail);
        }

        // POST: /ShoppingCartDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShoppingCartDetail shoppingcartdetail = await db.ShoppingCartDetails.FindAsync(id);
            db.ShoppingCartDetails.Remove(shoppingcartdetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
