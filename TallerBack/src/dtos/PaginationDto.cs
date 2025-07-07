using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerBack.src.dtos
{
    public class PaginationDto<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; } = new();
    }
}