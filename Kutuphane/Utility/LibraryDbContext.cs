using Kutuphane.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kutuphane.Utility
{
    public class LibraryDbContext : IdentityDbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<BookType> bookTypes { get; set; }
        public DbSet<Book> book { get; set; }
        public DbSet<Borrow> borrows { get; set; }
    }
}
