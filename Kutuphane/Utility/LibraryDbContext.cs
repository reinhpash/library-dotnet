using Kutuphane.Models;
using Microsoft.EntityFrameworkCore;

namespace Kutuphane.Utility
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
            
        }

        public DbSet<BookType> bookTypes { get; set; }
    }
}
