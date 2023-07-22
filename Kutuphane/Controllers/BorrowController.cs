using Kutuphane.Models;
using Kutuphane.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kutuphane.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BorrowController(IBorrowRepository ctx, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _borrowRepository = ctx;
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Borrow> borrows = _borrowRepository.GetAll(includeProps: "Book").ToList();

            return View(borrows);
        }

        public IActionResult AddOrEdit(int? ID)
        {
            IEnumerable<SelectListItem> bookList = _bookRepository.GetAll().Select(g => new SelectListItem
            {
                Text = g.Title,
                Value = g.ID.ToString(),

            }).ToList();

            ViewBag.BookList = bookList;

            if (ID == null || ID == 0)
            {
                return View();
            }
            else
            {
                Borrow? _borrow = _borrowRepository.Get(u => u.ID == ID);

                if (_borrow == null)
                    return NotFound();


                return View(_borrow);
            }

        }

        [HttpPost]
        public IActionResult AddOrEdit(Borrow _borrow)
        {
            if (ModelState.IsValid)
            {
                if (_borrow.ID == 0)
                {
                    _borrowRepository.Add(_borrow);
                    TempData["succses"] = _borrow.FirstName+" "+ _borrow.LastName + " successfully borrowed a book";
                }
                else
                {
                    _borrowRepository.Edit(_borrow);
                    TempData["succses"] = _borrow.FirstName + " " + _borrow.LastName + " successfully edited a borrow";
                }


                _borrowRepository.Save();
                return RedirectToAction("Index", "Borrow");
            }
            else
            {
                return View();
            }

        }

        public IActionResult Delete(int? ID) //? nullable
        {
            IEnumerable<SelectListItem> bookList = _bookRepository.GetAll().Select(g => new SelectListItem
            {
                Text = g.Title,
                Value = g.ID.ToString(),

            }).ToList();

            ViewBag.BookList = bookList;

            if (ID == null || ID == 0)
                return NotFound();

            Borrow? _borrow = _borrowRepository.Get(u => u.ID == ID);

            if (_borrow == null)
                return NotFound();

            return View(_borrow);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Borrow? selectedBorrow = _borrowRepository.Get(u => u.ID == id);

            if (selectedBorrow == null)
                return NotFound();



            _borrowRepository.Remove(selectedBorrow);
            _borrowRepository.Save();
            TempData["succses"] = "Successfully deleted a borrow!";
            return RedirectToAction("Index", "Borrow");
        }
    }
}