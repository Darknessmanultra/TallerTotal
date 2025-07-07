using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.dtos
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
        public int Total => Items.Sum(i => i.PriceAtPurchase * i.Quantity);
    }
    
    public class OrderItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int PriceAtPurchase { get; set; }
        public int Quantity { get; set; }
    }
}