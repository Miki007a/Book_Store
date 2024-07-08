using Book_Store.Models.DTO;
using Book_Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart AddBookToShoppingCart(string userId, AddToCartDTO model);
        AddToCartDTO getBookInfo(int Id);
        ShoppingCartDTO getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, int? Id);

        Boolean orderBooks(string userId);




    }
}
