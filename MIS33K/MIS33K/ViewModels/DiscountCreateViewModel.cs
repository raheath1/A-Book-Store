using MIS33K.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS33K.ViewModels
{
    public class DiscountCreateViewModel
    {
        public int DiscountCreateViewModelID { get; set; }
        public int DiscountID { get; set; }
        public decimal CustomerDiscountedPrice { get; set; }

        public Boolean DiscountEnabled { get; set; }

        public Book Book { get; set; }

        [Required]
        public int SelectedBook { get; set; }

    }
}