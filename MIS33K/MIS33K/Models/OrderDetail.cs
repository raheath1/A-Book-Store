using MIS33K.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIS33k.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
       
        public int Quantity { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal PriceLastPaid { get; set; }
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }

        public decimal ProcurementCost { get; set; }
    }
}