using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOP.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookRepository bookRepository { get; set; }
        private ILogService logger { get; set; }
        public BookController(IBookRepository book, ILogService logService)
        {
            bookRepository = book;
        }

        [HttpGet]
        public async Task<List<Book>> Get()
        {
            try
            {
                logger.LogInfo("Begin BookController.Get");
                var result = await bookRepository.GetBooks();
                logger.LogInfo("Success BookController.Get");
                return result;
            }catch(Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<Book> Post(string title, string author)
        {
            try
            {
                logger.LogInfo($"Begin BookController.Post => Title: {title}, Author: {author}");
                var result = await bookRepository.Add(title, author);
                logger.LogInfo("Success BookController.Post");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        [HttpPut]
        public async Task<Book> Put(Book book)
        {
            try
            {
                logger.LogInfo($"Begin BookController.Put => Title: {book?.Title}, Author: {book?.Author}");
                var result = await bookRepository.Update(book);
                logger.LogInfo("Success BookController.Put");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }
    }
}