namespace Kutuphane.Models
{
    public interface IBorrowRepository : IRepository<Borrow>
    {
        void Edit(Borrow borrow);
        void Save();
    }
}