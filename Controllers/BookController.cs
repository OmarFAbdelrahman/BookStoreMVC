using BookStoreMVC.Models;
using BookStoreMVC.Models.Repositories;
using BookStoreMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookStoreMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepo<Book> bookRepository;
        private readonly IBookStoreRepo<Author> authorRepository;

        public BookController(IBookStoreRepo<Book> bookRepository, IBookStoreRepo<Author> authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View(ModelWithFilledList());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please choose the name of the author";

                        return View(ModelWithFilledList());
                    }

                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author
                    };
                    bookRepository.Add(book);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("","Add all required fields");
                return View(ModelWithFilledList());
            }
            
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.Author.Id,
                Authors = authorRepository.List()
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {

                        Title = model.Title,
                        Description = model.Description,
                        Author = author
                    };
                    bookRepository.Update(model.BookId, book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Add all required fields");
                return View(ModelWithFilledList());
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FillList()
        {
            var authorShownValue = new Author
            {
                Id = -1,
                Name = "---Please Choose an author---"

            };
            var authors = authorRepository.List().ToList();

            if (authors[0].Id != -1)
                authors.Insert(0, authorShownValue);
            return authors;
        }

        BookAuthorViewModel ModelWithFilledList()
        {
            var viewModel = new BookAuthorViewModel
            {
                Authors = FillList()
            };
            return viewModel;
        }
    }
}
