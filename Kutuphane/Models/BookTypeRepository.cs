using Kutuphane.Utility;
using System.Linq.Expressions;

namespace Kutuphane.Models
{
    public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
    {
        private readonly LibraryDbContext libraryDbContext;
        public BookTypeRepository(LibraryDbContext _libraryDbContext) : base(_libraryDbContext)
        {
            libraryDbContext = _libraryDbContext;
        }

        public void Edit(BookType bookType)
        {
            libraryDbContext.Update(bookType);
        }

        public void Save()
        {
            libraryDbContext.SaveChanges();
        }
    }
}
