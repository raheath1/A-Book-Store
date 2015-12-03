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
using MIS33k.Models;
using MIS33K.ViewModels;
using MIS33K.Utilities;

namespace MIS33K.Controllers
{
    public class DiscountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Discount/
        public ActionResult Index()
        {
            return View(db.Discounts.ToList());
        }

        // GET: /Discount/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = await db.Discounts.FindAsync(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // GET: /Discount/Create
        public ActionResult Create()
        {
            DiscountCreateViewModel newDiscount = new DiscountCreateViewModel();
            ViewBag.allBooks = UpdateBooks.GetAllBooks(db);
            return View(newDiscount);
        }

        //// GET: /Discount/Create
        //public ActionResult Create()
        //{
        //    var BookLst = new List<string>();
        //    var BookQry = from d in db.Books
        //                  select d.Title;
        //    BookLst.AddRange(BookQry.Distinct());

        //    ViewBag.DiscountedBook = new SelectList(BookLst);
        //    var Books = from b in db.Books
        //                select b;
        //    return View();
        //}

        // POST: /Discount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiscountID,DiscountBook,Book,Books,DiscountedBook,SelectedBook,SelectList,BookLst,DiscountEnabled,CustomerDiscountedPrice,DetermineCustomerPrice")] Discount discount)
        {

            ViewBag.Title = new List<Book>();
            if (ModelState.IsValid)
            {
                //add book based on id
                discount.Book = db.Books.FirstOrDefault(b => b.BookID == discount.SelectedBook);
                db.Discounts.Add(discount);
                foreach (Book b in db.Books)
                {
                    b.CustomerPrice = b.findDiscount();
                }
                db.SaveChanges();
                return View(discount);
            }

            return View(discount);
        }

        //// POST: /Discount/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "DiscountID,DiscountBook,Book,Books,DiscountedBook,SelectedBook,SelectList,BookLst,DiscountEnabled,CustomerDiscountedPrice,DetermineCustomerPrice")] Discount discount)
        //{

        //    ViewBag.Title = new List<Book>();
        //    if (ModelState.IsValid)
        //    {
        //        db.Discounts.Add(discount);
        //        foreach (Book b in db.Books)
        //        {
        //            b.CustomerPrice = b.findDiscount();
        //        }
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(discount);
        //}

        // GET: /Discount/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = await db.Discounts.FindAsync(id);
            if (discount == null)
            {
                return HttpNotFound();
            }

            ViewBag.allBooks = UpdateBooks.GetAllBooks(db);
            return View(discount);
        }

        // POST: /Discount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DiscountID,DiscountBook,Book,Books,DiscountedBook,SelectedBook,DiscountEnabled,CustomerDiscountedPrice,DetermineCustomerPrice")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(discount);
        }

        // GET: /Discount/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = await db.Discounts.FindAsync(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: /Discount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Discount discount = await db.Discounts.FindAsync(id);
            db.Discounts.Remove(discount);
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
