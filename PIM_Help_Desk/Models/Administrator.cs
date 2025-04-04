﻿using PIM_Help_Desk.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PIM_Help_Desk.Models
{
    public class Administrator
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public string Position { get; set; } = string.Empty;
        public StatusUserEnum Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
    }
}
