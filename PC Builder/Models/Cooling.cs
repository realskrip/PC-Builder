﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PC_Builder.Models
{
    public class Cooling
    {
        [Key]
        public int CoolingId { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        public string? Category { get; set; }
    }
}