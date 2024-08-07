﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.Models
{
    public class Order : BaseEntity
    {
        public string? BookUserId { get; set; }
        public BookUser? BookUser { get; set; }

        public List<BooksInOrder>? BooksInOrders { get; set; }

        public DateTime? Created { get; set; }

        public Order()
        {
            Created=DateTime.Now;
            BooksInOrders = new List<BooksInOrder>();
        }


    }
}
