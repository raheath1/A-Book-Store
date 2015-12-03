using MIS33K.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS33k.Models
{
    [Bind(Exclude = "OrderID")]
    public partial class Order
    {
        [ScaffoldColumn(false)]
        public int OrderID { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }

        public virtual ApplicationUser Customer { get; set; }
        public decimal ShoppingCartSubTotal { get; set; }
        public decimal CartTotalWShipping { get; set; }
        public decimal ProcurementCost { get; set; }
        
        //display user information from the identity model

      

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Book> Books { get; set; }
    }
}