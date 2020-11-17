using BookStoreMVC.Models;
using BookStoreMVC.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookStoreRepo<Author> _authorRepository;

        public AuthorController(IBookStoreRepo<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: HomeController1
        public ActionResult Index()
        {
            var authors = _authorRepository.List();
            return View(authors);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            var author = _authorRepository.Find(id);
            return View(author);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                _authorRepository.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            var author = _authorRepository.Find(id);
            return View(author);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                _authorRepository.Update(id, author);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            var author = _authorRepository.Find(id);

            return View(author);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                _authorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
