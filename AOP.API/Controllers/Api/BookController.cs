using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOP.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookRepository bookRepository { get; set; }

        public BookController(IBookRepository book)
        {
            bookRepository = book;
        }

        [HttpGet]
        public async Task<List<Book>> Get()
        {
            var result = await bookRepository.GetBooks();
            return result;
        }

        [HttpPost]
        public async Task<Book> Post(string title, string author)
        {
            var result = await bookRepository.Add(title, author);
            return result;
        }

        [HttpPut]
        public async Task<Book> Put(Book book)
        {
            var result = await bookRepository.Update(book);
            return result;
        }
    }
}