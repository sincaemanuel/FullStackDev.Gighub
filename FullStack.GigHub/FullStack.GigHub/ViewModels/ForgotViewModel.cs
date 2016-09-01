﻿using System.ComponentModel.DataAnnotations;

namespace FullStack.GigHub.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}