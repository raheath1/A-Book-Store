using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS33K.Models;

namespace MIS33K.ViewModels
{
    public class BookIndexData
    {
        public IEnumerable<Book> Books { get; set; }
    }
}