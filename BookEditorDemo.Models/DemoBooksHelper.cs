using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public class DemoBooksHelper
    {
        private IBooksRepository _booksRepository;
        private IAuthorsRepository _authorsRepository;
        public DemoBooksHelper(IBooksRepository booksRepository, IAuthorsRepository authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
        }

        public void AddDemoBooks()
        {
            var jr = new Author("Joanne", "Rowling");
            var leo = new Author("Leo", "Tolstoy");
            var bs = new Author("Brandon", "Sanderson");
            var rm = new Author("Robert", "Martin");

            _authorsRepository.AddAuthor(jr);
            _authorsRepository.AddAuthor(leo);
            _authorsRepository.AddAuthor(bs);
            _authorsRepository.AddAuthor(rm);

            _booksRepository.AddBook(new Book("Harry Potter", 455, new DateTime(2005, 1, 1), "9780545010221", new[] { jr }));
            _booksRepository.AddBook(new Book("War and Peace", 1311, new DateTime(2011, 1, 1), "9781400079988", new[] { leo }));
            _booksRepository.AddBook(new Book("Mistborn", 670, new DateTime(2013, 1, 1), "9780765365439", new[] { bs }, "Tor Books"));
            _booksRepository.AddBook(new Book("Oathbringer", 1051, new DateTime(2017, 1, 1), "9781250297143", new[] { bs }, "Tor Books"));
            _booksRepository.AddBook(new Book("Clean Code", 522, new DateTime(2008, 1, 1), "9780132350884", new[] { rm }, "Эксмо"));
        }
    }
}
