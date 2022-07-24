using System;
using System.Collections.Generic;

namespace Film_Management_System_API.Models
{
    public partial class Customer
    {
        public string CustomerId { get; set; } = null!;
        public string CustName { get; set; } = null!;
        public string CustMobileNo { get; set; } = null!;
        public string CustEmail { get; set; } = null!;
        public string CustPassword { get; set; } = null!;
    }
}
