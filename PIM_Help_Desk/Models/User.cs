﻿using PIM_Help_Desk.Enums;
using System.ComponentModel.DataAnnotations;

namespace PIM_Help_Desk.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]  
        public string Email { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public UserTypeEnum UserType { get; set; } = UserTypeEnum.Administrator;
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
    }
}
