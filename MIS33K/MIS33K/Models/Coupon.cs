using MIS33K.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MIS33k.Models
{

        public class Coupon
        {
            //create new coupon code
            [Key]
            [Required]
            [Display(Name = "Coupon Code")]
            [StringLength(20, MinimumLength = 1, ErrorMessage = "Whoops, your Coupon Code should 1-20 digits!")]
            public string CouponID { get; set; }
            public static bool IsAllLettersOrDigits(string s)
            {
                foreach (char c in s)
                {
                    if (!Char.IsLetterOrDigit(c))
                        return false;
                }
                return true;
            }

            public virtual List<Book> Books { get; set; }


            //minimum amount for free shipping
            [Display(Name = "Minimum Amount for Free Shipping")]
            [DataType(DataType.Currency)]
            public decimal FreeShipMinimum { get; set; }

            //percent off all books in cart
            [Display(Name = "Percent Off for Total Order")]
            [DisplayFormat(DataFormatString = "{0:P2}")]
            public decimal PercentOff { get; set; }

            //discount on or off
            public Boolean DiscountEnabled { get; set; }

            [Required]
            [Display(Name = "Coupon Type")]
            public CouponType CouponTypeVar { get; set; }
            public enum CouponType
            {
                FreeShipMinimum,
                PercentOff,
            }


            public string CouponEntered { get; set; }

            //IDK if this method is right anymore
            public CouponType theCouponType { get; set; }

            public void ApplyCouponCode()
            {

                ShoppingCart CartObject = new ShoppingCart();
                if (CouponEntered == CouponID)
                {
                    if (theCouponType == CouponType.FreeShipMinimum && CouponEnabled == true)
                    {
                        CartObject.ShippingCost = 0;
                    }
                    else if (theCouponType == CouponType.PercentOff)
                    {
                        CartObject.CartSubTotal = CartObject.CartSubTotal * (1 - PercentOff);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            
            //create an instance of cart and book classes
            ShoppingCart ShoppingCartObject = new ShoppingCart();

            //coupon must be turned on or off
            [Required]
            [Display(Name = "Turn on this coupon?")]
            public Boolean CouponEnabled { get; set; }
        }
}


