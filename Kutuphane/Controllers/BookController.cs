using Kutuphane.Models;
using Kutuphane.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kutuphane.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookTypeRepository _bookTypeRepository;

        public BookController(IBookRepository ctx, IBookTypeRepository bookTypeRepository)
        {
            _bookRepository = ctx;
            _bookTypeRepository = bookTypeRepository;
        }
        public IActionResult Index()
        {
            List<Book> book = _bookRepository.GetAll().ToList();

            return View(book);
        }

        public IActionResult AddOrEdit(int? ID)
        {
            IEnumerable<SelectListItem> bookGenreList = _bookTypeRepository.GetAll().Select(g => new SelectListItem
            {
                Text = g.type,
                Value = g.genreID.ToString(),

            }).ToList();

            ViewBag.GenreList = bookGenreList;

            if (ID == null || ID == 0)
            {
                return View();
            }
            else
            {
                Book? _book = _bookRepository.Get(u => u.bookID == ID);

                if (_book == null)
                    return NotFound();


                return View(_book);
            }

        }

        [HttpPost]
        public IActionResult AddOrEdit(Book _book, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Add(_book);
                _bookRepository.Save();
                TempData["succses"] = "New book successfully added!";
                return RedirectToAction("Index", "Book");
            }
            else
            {
                return View();
            }

        }

        /* [HttpPost]
         public IActionResult Add(Book _book)
         {
             if (ModelState.IsValid)
             {
                 _bookRepository.Add(_book);
                 _bookRepository.Save();
                 TempData["succses"] = "New book successfully added!";
                 return RedirectToAction("Index", "Book");
             }
             else
             {
                 return View();
             }

         }*/



        public IActionResult Delete(int? ID) //? nullable
        {
            if (ID == null || ID == 0)
                return NotFound();

            Book? _book = _bookRepository.Get(u => u.bookID == ID);

            if (_book == null)
                return NotFound();




            return View(_book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Book? selectedBook = _bookRepository.Get(u => u.bookID == id);

            if (selectedBook == null)
                return NotFound();

            _bookRepository.Remove(selectedBook);
            _bookRepository.Save();
            TempData["succses"] = "Successfully deleted a book!";
            return RedirectToAction("Index", "Book");
        }
    }
}
