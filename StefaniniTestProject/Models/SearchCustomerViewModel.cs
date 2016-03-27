using System.Web.Mvc;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
namespace StefaniniTestProject.Models
{
    public class SearchCustomerViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Gender")]
        public Nullable<Gender> Gender { get; set; }

        [Display(Name = "City")]
        public string CityId { get; set; }

        [Display(Name = "Region")]
        public string RegionId { get; set; }

        [Display(Name = "Last purchase")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/YYYY}")]
        public Nullable<DateTime> LastPurchase { get; set; }

        [Display(Name = "Until")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:dd/MM/YYYY}")]
        public Nullable<DateTime> Until { get; set; }

        [Display(Name = "Classification")]
        public string ClassificationId { get; set; }

        [Display(Name = "Seller")]
        public string SellerId { get; set; }
    }
}