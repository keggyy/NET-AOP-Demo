using DAL.Entities;
using Repository.AOP_DynamicProxy.AOP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AOP_DynamicProxy
{
    public interface IBookRepository
    {
        [Query("SELECT * FROM BOOKS")]
        public List<Book> GetBooks();

        [Query("INSERT INTO BOOKS(TITLE, AUTHOR) VALUES('{0}', {1}")]
        public Book Add(string title, string author);

        public Book Update(Book book);
    }
}
