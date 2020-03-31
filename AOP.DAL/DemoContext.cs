using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DemoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;initial catalog=AOPDemo;Trusted_Connection=True;");
        }

        public DbSet<Book> Books { get; set; }
    }
}