using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetBooks();

        public Task<Book> Add(string title, string author);

        public Task<Book> Update(Book book);
    }
}