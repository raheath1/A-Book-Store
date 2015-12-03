using MIS33K.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS33k.Models
{
    public class Discount
    {

        public int DiscountID { get; set; }

        [Display(Name = "Select Book to Discount")]
        public int DiscountedBook { get; set; }
        public virtual Book Book { get; set; }

        //public List<Book> Books { get; set; }

        [Required]
        public int SelectedBook { get; set; }




        //Enable the discount 
        [Display(Name = "Enable this discount?")]
        public Boolean DiscountEnabled { get; set; }

        //Create an instances of cart and book classes
        //Book BookObject = new Book();
        ShoppingCart ShoppingCartObject = new ShoppingCart();

        //Check that the discount is less than regular price and it is turned 'on'
        //Enter new book price
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "New Discounted Price")]
        public decimal CustomerDiscountedPrice { get; set; }

        public decimal DetermineCustomerPrice(Boolean discountEnabled, decimal discountedPrice, decimal regularPrice)
        {
            decimal result;
            if (discountEnabled = true && discountedPrice < regularPrice)
                result = discountedPrice;
            else
                result = regularPrice;
            return result;
        }


       
    }
}