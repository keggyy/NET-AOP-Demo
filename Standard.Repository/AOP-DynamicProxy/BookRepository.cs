using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AOP_DynamicProxy
{
    public class BookRepository
    {
        public virtual Book Update(Book book)
        {
            using (var context = new DemoContext())
            {
                var entity = context.Books.Update(book);
                entity.State = EntityState.Modified;
                var res = context.SaveChangesAsync().Result;
                return entity.Entity;
            }
        }
    }
}
