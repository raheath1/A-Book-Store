using MIS33k.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIS33K.Models
{
    public class ShoppingCartDetail
    {
        public int ShoppingCartDetailID { get; set; }

        public ApplicationUser Customer { get; set;  }

        public int Quantity { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal PriceLastPaid { get; set; }
        public virtual Book Book { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Order Order { get; set; }

        
    }
}