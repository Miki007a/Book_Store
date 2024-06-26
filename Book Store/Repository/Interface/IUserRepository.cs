using Book_Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<BookUser> GetAll();
        BookUser Get(string id);
        void Insert(BookUser entity);
        void Update(BookUser entity);
        void Delete(BookUser entity);
    }

}
