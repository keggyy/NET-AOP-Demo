using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Standard
{
    public class BookRepository : IBookRepository
    {
        private DemoContext context { get; set; }
        private ILogService log { get; set; }

        public BookRepository(DemoContext demoContext, ILogService logService)
        {
            context = demoContext;
            log = logService;
        }

        public async Task<Book> Add(string title, string author)
        {
            log.LogInfo($"BookRepository.Add => {title}, {author}");
            try
            {
                var entity = await context.Books.AddAsync(new Book
                {
                    Author = author,
                    Title = title
                });
                entity.State = EntityState.Added;
                context.SaveChanges();
                log.LogInfo($"Operation success");
                return entity.Entity;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public async Task<List<Book>> GetBooks()
        {
            log.LogInfo("BookRepository.GetBooks");
            try
            {
                var result = await context.Books.ToListAsync();
                log.LogInfo($"Operation success");
                return result;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }

        public async Task<Book> Update(Book book)
        {
            log.LogInfo($"BookRepository.Update => {book.Title}, {book.Author}");
            try
            {
                var entity = context.Books.Update(book);
                entity.State = EntityState.Modified;
                await context.SaveChangesAsync();
                log.LogInfo($"Operation success");
                return entity.Entity;
            }
            catch (Exception ex)
            {
                log.LogError(ex);
                throw;
            }
        }
    }
}