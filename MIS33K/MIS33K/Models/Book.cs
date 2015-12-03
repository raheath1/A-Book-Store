using MIS33k.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MIS33K.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Display (Name = "SKU")]
        public string BookSKU { get; set; }
        public String Title { get; set; }

        [Display(Name="Author First Name")]
        public String AuthorFName { get; set; }

        [Display(Name = "Author Last Name")]
        public String AuthorLName { get; set; }

       

        [Display(Name = "Purchase History")]
        public Int32 PurchaseHistory { get; set; }

        [Display(Name = "Profit Margin")]
        public decimal ProfitMargin { get; set; }

       
        public decimal CustomerPrice { get; set; }

        public decimal cost { get; set; }
      
        

        [Column(TypeName = "DateTime2")]
        public DateTime PublicationDate { get; set; }
        public String Genre { get; set; }

      

      

        [Display(Name = "Stock on Hand")]
        public Int32 CountonHand { get; set; }

        [Display (Name= "Reorder Point")]
        public Int32 ReoderPoint { get; set; }

        [Display (Name="Amount to Order")]
        public Int32 ProcurementShoppingCart { get; set; }

        [Display(Name = "Pending Order")]
        public Int32 PendingOrder { get; set; }

        [Display(Name = ("Amount Recieved"))]

        public Int32 OrderAmountRecieved { get; set; }


        public decimal CustomerRegularPrice { get; set; }
        public decimal CustomerDiscountedPrice { get; set; }

        public List<BookReview> BookReview { get; set; }

        public decimal findDiscount()
        {
            decimal p = 0.0m;
            Discount d = new Discount();
            p = d.DetermineCustomerPrice(d.DiscountEnabled, d.CustomerDiscountedPrice, CustomerRegularPrice);
            return p;
        }

        public Book()
        { }


    }
}