using Kutuphane.Utility;

namespace Kutuphane.Models
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly LibraryDbContext libraryDbContext;
        public BookRepository(LibraryDbContext _libraryDbContext) : base(_libraryDbContext)
        {
            libraryDbContext = _libraryDbContext;
        }

        public void Edit(Book book)
        {
            libraryDbContext.Update(book);
        }

        public void Save()
        {
            libraryDbContext.SaveChanges();
        }
    }
}
