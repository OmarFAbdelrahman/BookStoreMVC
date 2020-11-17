using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.Models.Repositories
{
    public class BookRepository : IBookStoreRepo<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id = 1, Title = "Unknown", Description = "Nothing",
                    Author = new Author{ Id = 2}
                },
                new Book
                {
                    Id = 2, Title = "Campione", Description = "No data",
                    Author = new Author()
                },
                new Book
                {
                    Id = 3, Title = "Kingsmen", Description = "None",
                    Author = new Author()
                }

            };
        }
        public void Add(Book entity)
        {

            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);

        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public List<Book> List()
        {
            return books;
        }

        public void Update(int id, Book entity)
        {
            var book = Find(id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
        }
        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }
    }
}
