using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS33K.Models
{
    public class BookReview
    {
        [Key]
        public int BookReviewID { get; set; }

        public Book BookID { get; set; }


        [MaxLength( 100, ErrorMessage="Your review must be under 100 characters.")]
        public string review { get; set; }
    }
}