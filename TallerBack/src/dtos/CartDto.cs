using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.dtos
{
    public class CartDTO
    {
        public Guid CartId { get; set; }
        public List<CartItemDTO> Items { get; set; } = new();
        public int Total => Items.Sum(i => i.Price * i.Quantity);
    }

    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}