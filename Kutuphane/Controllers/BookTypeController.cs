using Kutuphane.Models;
using Kutuphane.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Kutuphane.Controllers
{
    public class BookTypeController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookTypeController(LibraryDbContext ctx)
        {
            libraryDbContext = ctx;
        }
        public IActionResult Index()
        {
            List<BookType> bookTypes = libraryDbContext.bookTypes.ToList();
            return View(bookTypes);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookType _bookType)
        {
            libraryDbContext.bookTypes.Add(_bookType);
            libraryDbContext.SaveChanges();
            return RedirectToAction("Index","BookType");
        }
    }
}
