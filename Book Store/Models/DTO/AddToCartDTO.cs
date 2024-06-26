using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.DTO
{
    public class AddToCartDTO
    {
        public int SelectedBookId { get; set; }
        public string? SelectedBookName { get; set; }
        public int Quantity { get; set; }

    }
}
