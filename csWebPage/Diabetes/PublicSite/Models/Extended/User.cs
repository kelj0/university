using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicSite.Models
{
    public partial class User
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "First name required")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LName { get; set; }

        [Display(Name = "Height")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Height is required")]
        public int Height { get; set; }

        [Display(Name = "Weight")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Weight is required")]
        public int Weight { get; set; }

        [Display(Name = "Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Birth { get; set; }

        /*dropdowns*/
        [Display(Name = "Sex")]
        public int Sex { get; set; }
        [Display(Name = "Activity")]
        public int Activity { get; set; }
        [Display(Name = "Diabetes type")]
        public int Diabetes_type { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }
    }

    public partial class UserAdditionalModel : User
    {
        public List<SelectListItem> s { get; set; }
        public List<SelectListItem> d{ get; set; }
        public List<SelectListItem> a { get; set; }
    }
}