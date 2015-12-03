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


namespace MIS33K.Controllers
{
    public class CouponController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Coupon/
        public async Task<ActionResult> Index()
        {
            return View(await db.Coupons.ToListAsync());
        }

        // GET: /Coupon/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = await db.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // GET: /Coupon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Coupon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CouponID,DiscountEnabled,CouponTypeVar,CouponEnabled,FreeShipMinimum,PercentOff")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                db.Coupons.Add(coupon);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(coupon);
        }


        // GET: /Coupon/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = await db.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // POST: /Coupon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CouponID,DiscountEnabled,CouponTypeVar,CouponEnabled,FreeShipMinimum,PercentOff")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coupon).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(coupon);
        }

        // GET: /Coupon/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = await db.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // POST: /Coupon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Coupon coupon = await db.Coupons.FindAsync(id);
            db.Coupons.Remove(coupon);
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
