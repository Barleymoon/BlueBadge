using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _14_GereralStore.Models
{
    public class TransactionCreate
    {
        [Key]
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string CustomerId { get; set; }
    }
}