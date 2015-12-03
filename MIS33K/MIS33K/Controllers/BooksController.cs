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
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public  ActionResult Index(string searchTitle, string searchGenre, string searchAuthorLastName, string searchAuthorFirstName, string searchSKU, SearchTypes? searchType, bool? ORSearch)
        {
            var GenreLst = new List<string>();
            var GenreQry = from d in db.Books
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.searchGenre = new SelectList(GenreLst);

            var books = from b in db.Books
                        select b;

            if (ORSearch == false)//this is an and search
            {
                if (!String.IsNullOrEmpty(searchTitle))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(s => s.Title.Contains(searchTitle));
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(s => s.Title == searchTitle);
                    }
                }

                if (!String.IsNullOrEmpty(searchGenre))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(x => x.Genre == searchGenre);
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(x => x.Genre == searchGenre);
                    }
                }
                if (!String.IsNullOrEmpty(searchAuthorLastName))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(a => a.AuthorLName.Contains(searchAuthorLastName));
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(a => a.AuthorLName == searchAuthorLastName);
                    }
                }
                if (!String.IsNullOrEmpty(searchAuthorFirstName))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(f => f.AuthorFName.Contains(searchAuthorFirstName));
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(f => f.AuthorFName == searchAuthorFirstName);
                    }
                }
                if (!String.IsNullOrEmpty(searchSKU))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(k => k.BookSKU == searchSKU);
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(k => k.BookSKU == searchSKU);
                    }
                }
            }
            else // or searches 
            {
                if (searchType == SearchTypes.KEYWORD)
                {
                    books = books.Where(p => p.Title.Contains(searchTitle) || p.AuthorFName.Contains(searchAuthorFirstName) || p.AuthorLName.Contains(searchAuthorLastName) || p.Genre == searchGenre || p.BookSKU == searchSKU);
                }
                else if (searchType == SearchTypes.EXACT)
                {
                    books = books.Where(p => p.Title == searchTitle || p.AuthorFName == searchAuthorFirstName || p.AuthorLName == searchAuthorLastName || p.Genre == searchGenre || p.BookSKU == searchSKU);
                }
            }


            return View(books);
        }

        //GET: Books/AutomaticProcurement
        public ActionResult AutomaticProcurement(int? reorderpoint, int? stockonhand)
        {
            var booksreorder = from b in db.Books
                               select b;
           
                booksreorder = booksreorder.Where(x => x.CountonHand + x.PendingOrder <= x.ReoderPoint && x.ProcurementShoppingCart == 0);
          
            return View(booksreorder);
        }

        //POST: Books/AutomaticProcurement[
        [HttpPost]
        public async Task<ActionResult> AutomaticProcurement([Bind(Include = "BookID,BookSKU,Title,AuthorFName,AuthorLName,InStock,CountonHand,PurchaseHistory,ProfitMargin,ReorderManual,ReorderAutomatic,CustomerPrice,cost,Discount,BookReviewID,PublicationDate,Genre,Description,UnitPrice,ReoderPoint,OrderAmountRecieved,PendingOrder,ProcurementShoppingCart")] Book book)
        {

            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                
                await db.SaveChangesAsync();

                return RedirectToAction("POrdersList");
            }


            return View(book);
        }

        //GET:books/AutomaticConfirm
        public async Task<ActionResult> AutomaticConfirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //Post: 
        [HttpPost]
         public async Task<ActionResult> AutomaticConfirm([Bind(Include = "BookID,BookSKU,Title,AuthorFName,AuthorLName,InStock,CountonHand,PurchaseHistory,ProfitMargin,ReorderManual,ReorderAutomatic,CustomerPrice,cost,Discount,BookReviewID,PublicationDate,Genre,Description,UnitPrice,ReoderPoint,OrderAmountRecieved,PendingOrder,ProcurementShoppingCart")] Book book)
        {
            var books = from b in db.Books
                        select b;
            if (ModelState.IsValid)
            {
                foreach( var BookID in  books.Where(x => x.CountonHand +x.PendingOrder < x.ReoderPoint && x.ProcurementShoppingCart == 0) )
                {
                    book.ProcurementShoppingCart = book.ProcurementShoppingCart;
                }
                db.Entry(book).State = EntityState.Modified;
                
                await db.SaveChangesAsync();
                
                return RedirectToAction("POrdersList");
            }
            
           
            return View(book);

      
      }


        //GET: Books/ProcurementPending
        public ActionResult ProcurementPending(Int32? CountonHand, Int32? OrderAmountRecieved, Int32? PendingOrder)
        {
            //var PendingLst = new List<string>();
            //var pendingQry = from d in db.Books
            //               orderby d.PendingOrder
            //               select d.PendingOrder;

            //var PendingList = 
            //ViewBag.searchPendingOrder = new SelectList(PendingLst);

            //for (Int32 i; i <= searchPendingOrder; i++)
            //{
            //    var pendinglist = new List<string>(i);
            //}
            var bookspending = from p in db.Books
                               select p;

            bookspending = bookspending.Where(q => q.PendingOrder != 0);

            if (OrderAmountRecieved != 0)
            {
                CountonHand = CountonHand + OrderAmountRecieved;
                PendingOrder = PendingOrder - OrderAmountRecieved;
                OrderAmountRecieved = 0;
                return View(bookspending);
            }

            return View(bookspending);
        }


        //GET: Books/POrdersList
        public ActionResult POrdersList()
        {
            var orderspending = from p in db.Books
                               select p;

            orderspending = orderspending.Where(q => q.ProcurementShoppingCart != 0);

            return View(orderspending);
        }

        
        //GET: Books/POrdersConfirm
        public async Task<ActionResult> POrdersConfirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        //Post: Books/POrdersConfirm
        [HttpPost]
        public async Task<ActionResult> POrdersConfirm([Bind(Include = "BookID,BookSKU,Title,AuthorFName,AuthorLName,InStock,CountonHand,PurchaseHistory,ProfitMargin,ReorderManual,ReorderAutomatic,CustomerPrice,cost,Discount,BookReviewID,PublicationDate,Genre,Description,UnitPrice,ReoderPoint,OrderAmountRecieved,PendingOrder,ProcurementShoppingCart")] Book book)
        {

            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                book.PendingOrder = book.PendingOrder + book.ProcurementShoppingCart;
                book.ProcurementShoppingCart = book.ProcurementShoppingCart - book.ProcurementShoppingCart;
                await db.SaveChangesAsync();
                
                return RedirectToAction("POrdersList");
            }
            
           
            return View(book);
        }

        //GET: books/ProcurementPendingCheckin/1
        public async Task<ActionResult> ProcurementPendingCheckIN(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //POST: Books/ProcurementPendingCheckIN
        [HttpPost]
        public async Task<ActionResult> ProcurementPendingCheckIN([Bind(Include = "BookID,BookSKU,Title,AuthorFName,AuthorLName,InStock,CountonHand,PurchaseHistory,ProfitMargin,ReorderManual,ReorderAutomatic,CustomerPrice,cost,Discount,BookReviewID,PublicationDate,Genre,Description,UnitPrice,ReoderPoint,OrderAmountRecieved,PendingOrder,ProcurementShoppingCart")] Book book)
        {

            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                if (book.OrderAmountRecieved <= book.PendingOrder && book.OrderAmountRecieved >= 0)
                {
                    book.PendingOrder = book.PendingOrder - book.OrderAmountRecieved;
                    book.CountonHand = book.CountonHand + book.OrderAmountRecieved;
                    book.OrderAmountRecieved = book.OrderAmountRecieved - book.OrderAmountRecieved;
                    await db.SaveChangesAsync();

                    return RedirectToAction("ProcurementPending");
                }
                else
                {
                    return RedirectToAction("ProcurementPending");
                }
            }


            return View(book);
        }


        // GET: books/ManualProcurementOrder
        public ActionResult ManualProcurementOrder(string searchTitle, string searchGenre, string searchAuthorLastName, string searchAuthorFirstName, string searchSKU, SearchTypes? searchType, bool? ORSearch)
        {
            var GenreLst = new List<string>();
            var GenreQry = from d in db.Books
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.searchGenre = new SelectList(GenreLst);

            var books = from b in db.Books
                        select b;

            if (ORSearch == false)//this is an and search
            {
                if (!String.IsNullOrEmpty(searchTitle))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(s => s.Title.Contains(searchTitle) && s.PendingOrder == 0);
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(s => s.Title == searchTitle && s.PendingOrder == 0);
                    }
                }

                if (!String.IsNullOrEmpty(searchGenre))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(x => x.Genre == searchGenre && x.PendingOrder == 0);
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(x => x.Genre == searchGenre && x.PendingOrder == 0);
                    }
                }
                if (!String.IsNullOrEmpty(searchAuthorLastName))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(a => a.AuthorLName.Contains(searchAuthorLastName) && a.PendingOrder == 0);
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(a => a.AuthorLName == searchAuthorLastName && a.PendingOrder == 0);
                    }
                }
                if (!String.IsNullOrEmpty(searchAuthorFirstName))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(f => f.AuthorFName.Contains(searchAuthorFirstName) && f.PendingOrder == 0);
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(f => f.AuthorFName == searchAuthorFirstName && f.PendingOrder == 0);
                    }
                }
                if (!String.IsNullOrEmpty(searchSKU))
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        books = books.Where(k => k.BookSKU == searchSKU && k.PendingOrder == 0);
                    }
                    else if (searchType == SearchTypes.EXACT)
                    {
                        books = books.Where(k => k.BookSKU == searchSKU && k.PendingOrder == 0);
                    }
                }
            }
            else // or searches 
            {
                if (searchType == SearchTypes.KEYWORD)
                {
                    books = books.Where(p => p.Title.Contains(searchTitle) || p.AuthorFName.Contains(searchAuthorFirstName) || p.AuthorLName.Contains(searchAuthorLastName) || p.Genre == searchGenre || p.BookSKU == searchSKU && p.PendingOrder == 0);
                }
                else if (searchType == SearchTypes.EXACT)
                {
                    books = books.Where(p => p.Title == searchTitle || p.AuthorFName == searchAuthorFirstName || p.AuthorLName == searchAuthorLastName || p.Genre == searchGenre || p.BookSKU == searchSKU && p.PendingOrder == 0);
                }
            }


            return View(books);
        }



        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookID,BookSKU,Title,AuthorFName,AuthorLName,InStock,CountonHand,PurchaseHistory,ProfitMargin,ReorderManual,ReorderAutomatic,CustomerPrice,cost,Discount,BookReviewID,PublicationDate,Genre,Description,UnitPrice")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        


        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

      

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookID,BookSKU,Title,AuthorFName,AuthorLName,InStock,CountonHand,PurchaseHistory,ProfitMargin,ReorderManual,ReorderAutomatic,CustomerPrice,cost,Discount,BookReviewID,PublicationDate,Genre,Description,UnitPrice")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }


  //GET: Books/POrdersBooks

        public async Task<ActionResult> POrderBooks (int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book =  await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }


        //Post: Books/POrdersBooks/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> POrderBooks([Bind(Include = "BookID,BookSKU,Title,AuthorFName,AuthorLName,InStock,CountonHand,PurchaseHistory,ProfitMargin,ReorderManual,ReorderAutomatic,CustomerPrice,cost,Discount,BookReviewID,PublicationDate,Genre,Description,UnitPrice,ReoderPoint,OrderAmountRecieved,PendingOrder,ProcurementShoppingCart")]Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ManualProcurementOrder");
            }
            return View(book);
        }


        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
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
