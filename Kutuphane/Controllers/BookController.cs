using Kutuphane.Models;
using Kutuphane.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kutuphane.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookTypeRepository _bookTypeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository ctx, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = ctx;
            _bookTypeRepository = bookTypeRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        [Authorize(Roles = "Admin,User")]
        public IActionResult Index()
        {
            //List<Book> book = _bookRepository.GetAll().ToList();
            List<Book> book = _bookRepository.GetAll(includeProps: "bookType").ToList();

            return View(book);
        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddOrEdit(int? ID)
        {
            IEnumerable<SelectListItem> bookGenreList = _bookTypeRepository.GetAll().Select(g => new SelectListItem
            {
                Text = g.type,
                Value = g.ID.ToString(),

            }).ToList();

            ViewBag.GenreList = bookGenreList;

            if (ID == null || ID == 0)
            {
                return View();
            }
            else
            {
                Book? _book = _bookRepository.Get(u => u.ID == ID);

                if (_book == null)
                    return NotFound();


                return View(_book);
            }

        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult AddOrEdit(Book _book, IFormFile? file)
        {
            //var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    _book.imageUrl = @"\img\" + file.FileName;
                }


                if (_book.ID == 0)
                {
                    _bookRepository.Add(_book);
                    TempData["succses"] = _book.Title + " successfully added!";
                }
                else
                {
                    _bookRepository.Edit(_book);
                    TempData["succses"] = _book.Title + " successfully edited!";
                }

               
                _bookRepository.Save();
                return RedirectToAction("Index", "Book");
            }
            else
            {
                return View();
            }

        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Delete(int? ID) //? nullable
        {
            if (ID == null || ID == 0)
                return NotFound();

            Book? _book = _bookRepository.Get(u => u.ID == ID);

            if (_book == null)
                return NotFound();




            return View(_book);
        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Book? selectedBook = _bookRepository.Get(u => u.ID == id);

            if (selectedBook == null)
                return NotFound();

            _bookRepository.Remove(selectedBook);
            _bookRepository.Save();
            TempData["succses"] = "Successfully deleted a book!";
            return RedirectToAction("Index", "Book");
        }
    }
}
