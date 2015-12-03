using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS33k
{
    public class CreditCard
    { [Required]
        [Key]
        public int CreditCardID { get; set; }


    public enum CardType
    {
        MasterCard,
        Visa,
        AmericanExpress,
        Discover
    }

        public string DefaultPayment { get; set; }
        public bool Card1 { get; set; }
        public CardType Card1Type { get; set; }
        public string Card1Num { get; set; }
        public bool Card2 { get; set; }
        public CardType Card2Type { get; set; }
        public string Card2Num { get; set; }
        public bool Card3 { get; set; }
        public CardType Card3Type { get; set; }
        public string Card3Num { get; set; }

        private void SelectDefaultPayment(object sender, EventArgs e)
        {
            if (Card1 == true)
            {
                DefaultPayment = Card1Num;
                return;
            }
            else if (Card2 == true)
            {
                DefaultPayment = Card2Num;
                return;
            }
            else
            {
                DefaultPayment = Card3Num;
                return;
            }
        }

    }
    }