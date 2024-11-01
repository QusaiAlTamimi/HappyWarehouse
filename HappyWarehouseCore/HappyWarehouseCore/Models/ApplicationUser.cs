﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HappyWarehouseCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; }

        public bool Active { get; set; } = true;
    }
}