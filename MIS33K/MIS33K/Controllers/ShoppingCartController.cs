using MIS33k.Models;
using MIS33K.Models;
using MIS33K.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MIS33K.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /ShoppingCart/
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.ShoppingCarts.ToListAsync());

        //}
        public ActionResult Index()
        {
            return View();

        }

        // GET: /ShoppingCart/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingcart = await db.ShoppingCarts.FindAsync(id);
            if (shoppingcart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingcart);
        }


        //Add to Cart 
        public ActionResult AddToCart(int id, ShoppingCart cart)
        {
            // Retrieve the book from the database
            var addedBook = db.Books.Single(books => books.BookID == id);

            cart.BookList.Add(addedBook);

            // Need to check that the person who is logged in is associated with cart
            //string userID = User.Identity.GetUserId<string>();
            IEnumerable<ShoppingCartDetail> CartDetailsList =
            from ShoppingCartDetails in db.ShoppingCartDetails
            //where ShoppingCartDetails.Customer = userID
            //where ShoppingCartDetails.Book.BookID = id
            select ShoppingCartDetails;

            if (CartDetailsList.Count() == 0)
            {
                //create shopping cart detail 
                ShoppingCartDetail CartDetails = new ShoppingCartDetail(); //update each of the properties of cart  details are updated
                CartDetails.Quantity = 1;
                CartDetails.CustomerPrice = addedBook.CustomerPrice;
                CartDetails.PriceLastPaid = addedBook.cost;

                //add the details to shopping cart details
                db.ShoppingCartDetails.Add(CartDetails);

                //save shopping cart details db set
                db.SaveChanges();

            }
            else
            {
                //update quant of shopping cart detail 
                CartDetailsList.ElementAt(0).Quantity++;

                //save shopping cart detail 
                db.SaveChanges();
            }

            // Get the updated counts and totals
            //ShoppingCart.CartCount;


            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        // GET: /ShoppingCart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ShoppingCart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ShoppingCartID,BookID,DateCreated,CartSubTotal,ShippingCost,OrderTotal,Quantity,CustomerPrice,PriceLastPaid,GetCount")] ShoppingCart shoppingcart)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCarts.Add(shoppingcart);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shoppingcart);
        }

        // GET: /ShoppingCart/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingcart = await db.ShoppingCarts.FindAsync(id);
            if (shoppingcart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingcart);
        }

        // POST: /ShoppingCart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ShoppingCartID,BookID,DateCreated,CartSubTotal,ShippingCost,OrderTotal,Quantity,CustomerPrice,PriceLastPaid,GetCount")] ShoppingCart shoppingcart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingcart).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shoppingcart);
        }

        // GET: /ShoppingCart/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingcart = await db.ShoppingCarts.FindAsync(id);
            if (shoppingcart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingcart);
        }

        // POST: /ShoppingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShoppingCart shoppingcart = await db.ShoppingCarts.FindAsync(id);
            db.ShoppingCarts.Remove(shoppingcart);
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