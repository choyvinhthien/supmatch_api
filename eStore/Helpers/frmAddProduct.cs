﻿using System.ComponentModel.DataAnnotations;
namespace eStore.Helpers
{
    public class frmAddProduct
    {
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInstock { get; set; }
        public string AccountId { get; set; }
        public IFormFileCollection ImageFile { get; set; } = null!;
    }
}
