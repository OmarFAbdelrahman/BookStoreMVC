using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.Models.Repositories
{
    public class AuthorRepository : IBookStoreRepo<Author>
    {
        List<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author
                {
                    Id = 1, Name = "Omar"
                },
                new Author
                {
                    Id = 2, Name = "Tarek"
                },
                new Author
                {
                    Id = 3, Name = "Ahmed"
                }
            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(a => a.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public List<Author> List()
        {
            return authors;
        }

        public void Update(int id, Author entity)
        {
            var author = Find(id);
            author.Name = entity.Name;
        }
    }
}
