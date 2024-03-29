﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MoviesAPI.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string PublicPlace { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public int Number { get; set; }

        [JsonIgnore]
        public virtual Theater Theater { get; set; }
    }
}
