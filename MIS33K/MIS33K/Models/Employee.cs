using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIS33k.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public Int32 EmployeeID { get; set; }

        [Required(ErrorMessage = "Please imput a valid first name.")]
        [Display(Name = "First Name")]
        public string EmployeeFName { get; set; }

        [Required(ErrorMessage = "Please input a valid last name.")]
        [Display(Name = "Last Name")]
        public string EmployeeLName { get; set; }

        [Required(ErrorMessage = "Please input a valid email.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmployeeEmail { get; set; }

        //add phone number
        [Required(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        //stolen from stackoverflow 
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Please enter a valid phone number.")]
        //this might not even work
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        public String PhoneNumber { get; set; }

        //I think all this gunk should format the password
        //stolen from generated account view model
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string EmployeePassword { get; set; }

        [Required]
        [Display(Name = "Fire?")]
        public bool EmployeeEnabled { get; set; }

        [Required]
        [Display(Name = "Promote to Manager?")]
        public bool EmployeePromoted { get; set; }
    }
}