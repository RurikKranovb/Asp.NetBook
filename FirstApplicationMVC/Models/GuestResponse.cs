﻿using System.ComponentModel.DataAnnotations;

namespace FirstApplicationMVC.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Please enter you name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please specify whether you will attend")]
        public bool? WillAttend { get; set; }
    }
}
