//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineStoreModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.CartItems = new HashSet<CartItem>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
        public bool Enabled { get; set; }
    
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
