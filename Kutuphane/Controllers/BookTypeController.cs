using Kutuphane.Models;
using Kutuphane.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Kutuphane.Controllers
{
    public class BookTypeController : Controller
    {
        private readonly LibraryDbContext _libraryDbContext;

        public BookTypeController(LibraryDbContext ctx)
        {
            _libraryDbContext = ctx;
        }
        public IActionResult Index()
        {
            List<BookType> bookTypes = _libraryDbContext.bookTypes.ToList();
            return View(bookTypes);
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
