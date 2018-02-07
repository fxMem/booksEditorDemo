using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace BookEditorDemo.Models
{
    public class InMemoryBooksRepository : IBooksRepository
    {
        // I thought of using ConcurentDictionary, but decided to go for simplicity.
        private List<Book> _books = new List<Book>();
        private object _lock = new object();
        private int _idSequence = 1;

        public void AddBook(Book book)
        {
            lock (_lock)
            {
                var existingBook = GetBookByISN(book.ISBN);
                if (existingBook != null)
                {
                    throw new ArgumentException($"Book with ISN {existingBook.ISBN} ({existingBook.Title}) already exists!");
                }

                book.Id = _idSequence++;
                _books.Add(book);
            }
        }

        public Book GetBook(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<BookInfo> GetBooks(PaginationData page)
        {
            var source = _books.AsEnumerable();
            switch (page.SortBy)
            {
                case SortByProperty.Title:
                    source = source.OrderByDirection(b => b.Title, page.OrderByDescending);
                    break;
                case SortByProperty.PublishYear:
                    source = source.OrderByDirection(b => b.PublicationDate, page.OrderByDescending);
                    break;
            }

            return source.Select(b => new BookInfo
            {
                Id = b.Id,
                Title = b.Title,
                PublicationYear = b.PublicationDate.Year,
                AuthorFirstName = b.FirstAuthorFirstName,
                AuthorLastName = b.FirstAuthorLastName
            }).Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize); ;
        }

        public void RemoveBook(int id)
        {
            lock (_lock)
            {
                _books.RemoveAll(b => b.Id == id);
            }
        }

        public void UpdateBook(Book book)
        {
            lock (_lock)
            {
                _books.RemoveAll(b => b.Id == book.Id);
                _books.Add(book);
            }
        }

        private Book GetBookByISN(string isn)
        {
            return _books.FirstOrDefault(b => string.Equals(b.ISBN, isn, StringComparison.OrdinalIgnoreCase));
        }
    }
}
