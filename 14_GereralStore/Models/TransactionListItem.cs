using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _14_GereralStore.Models
{
    public class TransactionListItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProductName { get; set; }
        public double TransactionAmount { get; set; }
    }
}