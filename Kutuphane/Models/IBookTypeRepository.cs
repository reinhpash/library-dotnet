namespace Kutuphane.Models
{
    public interface IBookTypeRepository : IRepository<BookType>
    {
        void Edit(BookType bookType);
        void Save();
    }
}
