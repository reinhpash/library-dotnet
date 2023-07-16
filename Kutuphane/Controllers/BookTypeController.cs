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
            if (ModelState.IsValid)
            {
                libraryDbContext.bookTypes.Add(_bookType);
                libraryDbContext.SaveChanges();
                return RedirectToAction("Index", "BookType");
            }
            else
            {
                return View();
            }

        }

        public IActionResult Edit(int? ID) //? nullable
        {
            if (ID == null || ID == 0)
                return NotFound();

            BookType? _bookType = libraryDbContext.bookTypes.Find(ID);

            if (_bookType == null)
                return NotFound();




            return View(_bookType);
        }

        [HttpPost]
        public IActionResult Edit(BookType _bookType)
        {
            if (ModelState.IsValid)
            {
                libraryDbContext.bookTypes.Update(_bookType);
                libraryDbContext.SaveChanges();
                return RedirectToAction("Index", "BookType");
            }
            else
            {
                return View();
            }

        }

        public IActionResult Delete(int? ID) //? nullable
        {
            if (ID == null || ID == 0)
                return NotFound();

            BookType? _bookType = libraryDbContext.bookTypes.Find(ID);

            if (_bookType == null)
                return NotFound();




            return View(_bookType);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            BookType? selectedBookType = libraryDbContext.bookTypes.Find(id);

            if (selectedBookType == null)
                return NotFound();

            libraryDbContext.bookTypes.Remove(selectedBookType);
            libraryDbContext.SaveChanges();
            return RedirectToAction("Index", "BookType");
        }
    }
}
