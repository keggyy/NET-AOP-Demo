using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.AOP_DynamicProxy;

namespace DynamicProxy.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Intercept("logs")]
    public class BookController : ControllerBase
    {
        private IBookRepository bookRepository { get; set; }

        public BookController(IBookRepository book)
        {
            bookRepository = book;
        }

        [HttpGet]
        public virtual async Task<List<Book>> Get()
        {
            var result = bookRepository.GetBooks();
            return await Task.Run (() => result);
        }

        [HttpPost]
        public virtual async Task<Book> Post(string title, string author)
        {
            var result = bookRepository.Add(title, author);
            return await Task.Run(() => result);
        }

        [HttpPut]
        public virtual async Task<Book> Put(Book book)
        {
            var result = bookRepository.Update(book);
            return await Task.Run(() => result);
        }
    }
}