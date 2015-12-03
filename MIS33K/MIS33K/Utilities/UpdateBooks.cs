using MIS33K.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS33K.Utilities
{
    public class UpdateBooks
    {
        public static SelectList GetAllBooks(ApplicationDbContext db)
        {
            var query = from b in db.Books
                        orderby b.Title
                        select b;
            List<Book> allBooks = query.ToList();

            SelectList list = new SelectList(allBooks, "BookID", "Title");

            return list;
        }

        public static SelectList GetAllBooksWithAll(ApplicationDbContext db)
        {
            var query = from b in db.Books
                        orderby b.Title
                        select b;
            List<Book> allBooks = query.ToList();

            //allBooks.Insert(0, new Book { BookID = -1, Title = "All Books"
            //});

            SelectList list = new SelectList(allBooks, "BookID", "Title");
            return list;
        }

        public static void AddOrUpdateBook(ApplicationDbContext context, IEnumerable<Book> Book)
        {
            AddOrUpdateBook(context, Book);
        }
    }
}