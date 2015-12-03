using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS33K.ViewModels
{
    public class BookCreateViewModel
    {
        public int BookID { get; set; }
        public string AuthorFName { get; set; }
        public string AuthorLName { get; set; }

        [Required]
        public string Title { get; set; }

        //Book chosen by user
        private List<int> _selectedBook { get; set; }

        [Required]
        public int SelectedBook { get; set; }

        public List<int> SelectedBooks
        {
            get
            {
                return _selectedBook ?? new List<int>();
            }
            set
            {
                _selectedBook = value;
            }
        }
    }
}