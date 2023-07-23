using Kutuphane.Models;
using Kutuphane.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kutuphane.Controllers
{

    [Authorize(Roles = UserRoles.Role_Admin)]
    public class BookTypeController : Controller
    {
        private readonly IBookTypeRepository _bookTypeRepository;

        public BookTypeController(IBookTypeRepository ctx)
        {
            _bookTypeRepository = ctx;
        }
        public IActionResult Index()
        {
            List<BookType> bookTypes = _bookTypeRepository.GetAll().ToList();
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
                _bookTypeRepository.Add(_bookType);
                _bookTypeRepository.Save();
                TempData["succses"] = "New genre successfully added!";
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

            BookType? _bookType = _bookTypeRepository.Get(u=> u.ID == ID);

            if (_bookType == null)
                return NotFound();




            return View(_bookType);
        }

        [HttpPost]
        public IActionResult Edit(BookType _bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Edit(_bookType);
                _bookTypeRepository.Save();
                TempData["succses"] = "Genre successfully edited!";
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

            BookType? _bookType = _bookTypeRepository.Get(u => u.ID == ID);

            if (_bookType == null)
                return NotFound();




            return View(_bookType);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            BookType? selectedBookType = _bookTypeRepository.Get(u => u.ID == id);

            if (selectedBookType == null)
                return NotFound();

            _bookTypeRepository.Remove(selectedBookType);
            _bookTypeRepository.Save();
            TempData["succses"] = "Successfully deleted a genre!";
            return RedirectToAction("Index", "BookType");
        }
    }
}
