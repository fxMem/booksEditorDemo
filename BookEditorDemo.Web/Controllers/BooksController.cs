using BookEditorDemo.Web.Models;
using BookEditorDemo.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BookEditorDemo.Web.Controllers
{
    public class BooksController: ControllerBase
    {
        private BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        public IEnumerable<BookInfoViewModel> Get()
        {
            // For simplicity, just getting all books
            var books = _booksService.GetBooks(new PageData { PageNum = 1, PageSize = int.MaxValue }, Settings.SortBy, Settings.OrderByDescending);
            return books;
        }
    }
}