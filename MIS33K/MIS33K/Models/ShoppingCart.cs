using MIS33k.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS33K.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartID { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public System.DateTime DateCreated { get; set; }

        public List<Book> Books { get; set; }

        public List<Book> BookList = new List<Book>();

        public List<ShoppingCartDetail> ShoppingCartDetails { get; set; }


        // Procurement cost
        public decimal PriceLastPaid { get; set; }

        public const string CartSessionKey = "ShoppingCartID";

        ApplicationDbContext storeDB = new ApplicationDbContext();




        public void EmptyCart()
        {
            var cartItems = storeDB.ShoppingCarts.Where(cart => cart.ShoppingCartID == ShoppingCartID);

            foreach (var cartItem in cartItems)
            {
                storeDB.ShoppingCarts.Remove(cartItem);
            }

            // Save changes
            storeDB.SaveChanges();
        }

        public List<ShoppingCart> GetCartItems()
        {
            return storeDB.ShoppingCarts.Where(cart => cart.ShoppingCartID == ShoppingCartID).ToList();
        }


        public int CartCount;
        public int GetCount
        {
            get { return CartCount; }
            set
            {
                ShoppingCartDetail ShoppingCartDetailsObject = new ShoppingCartDetail();
                CartCount = (from addedBooks in storeDB.ShoppingCartDetails
                             where addedBooks.ShoppingCartDetailID == ShoppingCartDetailsObject.ShoppingCartDetailID
                             select (int)addedBooks.Quantity).Sum();
            }
        }

        //Calc Order SubTotal
        public decimal? CartSubTotal;
        public decimal? CalcCartSubTotal
        {
            get { return CartSubTotal; }
            set
            {
                ShoppingCartDetail CartDetailsObject = new ShoppingCartDetail();
                CalcCartSubTotal = (from addedBooks in storeDB.ShoppingCarts
                                    where addedBooks.ShoppingCartID == ShoppingCartID
                                    select (int)CartDetailsObject.Quantity * CartDetailsObject.CustomerPrice).Sum();
            }
        }

        //Calc Shipping Cost
        public decimal ShippingCost;
        public decimal CalcShipping
        {
            get { return ShippingCost; }
            set
            {
                CalcShipping = Convert.ToDecimal(3.50) + Convert.ToDecimal(1.50 * (CartCount - 1));
            }
        }


        //Calc Order Total 
        public decimal? OrderTotal;
        public decimal? CalcOrderTotal
        {
            get { return OrderTotal; }
            set
            {
                CalcOrderTotal = ShippingCost + CartSubTotal;
            }
        }



        //Create an order from a shopping cart
        public int CreateOrder()
        {
            Order OrderObject = new Order();
            List<Book> BooksToOrder = new List<Book>();
            List<ShoppingCart> Items = new List<ShoppingCart>();
            Items = storeDB.ShoppingCarts.Where(cart => cart.ShoppingCartID == ShoppingCartID).ToList();
            foreach (var item in Items)
            {
                BooksToOrder = item.Books;

            }
            OrderObject.Books = BooksToOrder;
            OrderObject.OrderDate = System.DateTime.Now;
            storeDB.Orders.Add(OrderObject);

            //subtract from inventory
            Book BookObject = new Book();
            ShoppingCartDetail ShoppingCartDetailObject = new ShoppingCartDetail();
            BookObject.CountonHand = BookObject.CountonHand - ShoppingCartDetailObject.Quantity;

            // Save changes
            storeDB.SaveChanges();
            return OrderObject.OrderID;
        }

    }
}