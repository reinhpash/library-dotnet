using Kutuphane.Utility;

namespace Kutuphane.Models
{
    public class BorrowRepository : Repository<Borrow>, IBorrowRepository
    {
        private readonly LibraryDbContext libraryDbContext;
        public BorrowRepository(LibraryDbContext _libraryDbContext) : base(_libraryDbContext)
        {
            libraryDbContext = _libraryDbContext;
        }

        public void Edit(Borrow borrow)
        {
            libraryDbContext.Update(borrow);
        }

        public void Save()
        {
            libraryDbContext.SaveChanges();
        }
    }
}
