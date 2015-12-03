//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
////using SafariBookStore.Utilities;
//using SafariBookStore.Models;

//namespace SafariBookStore.ViewModels
//{
//    public static class ViewModelHelpers
//    {
//        public static BookCreateViewModel ToViewModel (this Book book)
//        {
//            var bookCreateViewModel = new BookCreateViewModel()
//            {
//                Title = book.Title,
//                BookID = book.BookID,
//                AuthorFName = book.AuthorFName,
//                AuthorLName = book.AuthorLName,
//            };

//            return bookCreateViewModel;
//        }

//            public static Book ToDomainModel(this BookCreateViewModel bookCreateViewModel)
//            {
//                var book = new Book(bookCreateViewModel.Title);
//                book.BookID = bookCreateViewModel.BookID;
//                book.AuthorFName = bookCreateViewModel.AuthorFName;
//                book.AuthorLName = bookCreateViewModel.AuthorLName;
//                return book;
//            }
//        }

//}
