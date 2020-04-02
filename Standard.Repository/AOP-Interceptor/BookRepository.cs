using Autofac.Extras.DynamicProxy;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.AOP_Interceptor
{
    [Intercept("logs")]
    public class BookRepository : IBookRepository
    {
        private DemoContext context { get; set; }

        public BookRepository(DemoContext demoContext)
        {
            context = demoContext;
        }

        public virtual async Task<Book> Add(string title, string author)
        {
            var entity = await context.Books.AddAsync(new Book
            {
                Author = author,
                Title = title
            });
            entity.State = EntityState.Added;
            context.SaveChanges();
            return entity.Entity;
        }

        public virtual async Task<List<Book>> GetBooks()
        {
            var result = await context.Books.ToListAsync();
            return result;
        }

        public virtual async Task<Book> Update(Book book)
        {
            var entity = context.Books.Update(book);
            entity.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity.Entity;
        }
    }
}