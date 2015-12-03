using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS33K;
using MIS33K.Models;
using MIS33k;

namespace MIS33K.Controllers
{
    public class CreditCardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /CreditCard/
        public async Task<ActionResult> Index()
        {
            return View(await db.CreditCards.ToListAsync());
        }

        // GET: /CreditCard/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditcard = await db.CreditCards.FindAsync(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            return View(creditcard);
        }

        // GET: /CreditCard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CreditCardID,DefaultPayment,Card1,Card1Type,Card1Num,Card2,Card2Type,Card2Num,Card3,Card3Type,Card3Num")] CreditCard creditcard)
        {
            if (ModelState.IsValid)
            {
                db.CreditCards.Add(creditcard);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(creditcard);
        }

        // GET: /CreditCard/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditcard = await db.CreditCards.FindAsync(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            return View(creditcard);
        }

        // POST: /CreditCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CreditCardID,DefaultPayment,Card1,Card1Type,Card1Num,Card2,Card2Type,Card2Num,Card3,Card3Type,Card3Num")] CreditCard creditcard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditcard).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(creditcard);
        }

        // GET: /CreditCard/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditcard = await db.CreditCards.FindAsync(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            return View(creditcard);
        }

        // POST: /CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CreditCard creditcard = await db.CreditCards.FindAsync(id);
            db.CreditCards.Remove(creditcard);
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
