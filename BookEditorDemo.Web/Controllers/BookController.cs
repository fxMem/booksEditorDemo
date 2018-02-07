using BookEditorDemo.Models;
using BookEditorDemo.Web.Models;
using BookEditorDemo.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BookEditorDemo.Web.Controllers
{
    public class BookController: ControllerBase
    {
        private BooksService _booksService;

        public BookController(BooksService booksService)
        {
            _booksService = booksService;
        }

        public BookViewModel Get(int id)
        {
            var book = _booksService.GetBook(id);
            return book;
        }

        public void Post([FromBody]CreateBookData data)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("You must specify book creation data!");
            }

            _booksService.AddBook(data);
        }

        public void Put(int id, [FromBody]UpdateBookData data)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("You must specify book update data!");
            }

            _booksService.UpdateBook(id, data);
        }

        public void Delete(int id)
        {
            _booksService.RemoveBook(id);
        }
    }
}