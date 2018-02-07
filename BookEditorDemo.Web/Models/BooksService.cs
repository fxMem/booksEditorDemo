using BookEditorDemo.Models;
using BookEditorDemo.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BookEditorDemo.Web.Models
{
    // Application service as mediator between domain model and controllers
    public class BooksService
    {
        private IBooksRepository _booksRepository;
        private IAuthorsRepository _authorsRepository;
        private IFilesRepository _filesRepository;

        public BooksService(
            IBooksRepository booksRepository, 
            IAuthorsRepository authorsRepository,
            IFilesRepository filesRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
            _filesRepository = filesRepository;
        }

        public IEnumerable<BookInfoViewModel> GetBooks(PageData pageData, SortByProperty sortBy, bool descreasing)
        {
            return _booksRepository.GetBooks(new PaginationData
            {
                PageNumber = pageData.PageNum,
                PageSize = pageData.PageSize,
                SortBy = sortBy,
                OrderByDescending = descreasing
            }).Select(bi => new BookInfoViewModel
            {
                Id = bi.Id,
                Title = bi.Title,
                AuthorFirstName = bi.AuthorFirstName,
                AuthorLastName = bi.AuthorLastName,
                PublicationYear = bi.PublicationYear
            });
        }

        public BookViewModel GetBook(int bookId)
        {
            var book = _booksRepository.GetBook(bookId);
            if (book == null)
            {
                throw new ArgumentException($"Book with id {bookId} not found!");
            }

            // For simplicity, I'm not using AutoMapper and simular tools
            return new BookViewModel
            {
                Id = book.Id,
                ISBN = book.ISBN,
                PageNumber = book.PageNumber,
                Title = book.Title,
                PublishYear = book.PublicationDate.Year,
                Publisher = book.Publisher,
                HaveCover = book.HaveCoverImage,
                Authors = book.Authors.Select(a =>
                    new AuthorViewModel
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    }).ToList()
            };
        }

        public void AddBook(CreateBookData data)
        {
            var authors = _authorsRepository.GetAuthors(data.AuthorIds);
            var book = new Book(data.Title, 
                data.PageNumber, 
                new DateTime(data.PublishYear, 1, 1), 
                data.ISBN,
                authors,
                data.Publisher
                );

            _booksRepository.AddBook(book);
        }

        public void RemoveBook(int bookId)
        {
            _booksRepository.RemoveBook(bookId);
        }

        public void UpdateBook(int bookId, UpdateBookData data)
        {
            var book = _booksRepository.GetBook(bookId);
            if (book == null)
            {
                throw new ArgumentException($"Cannot edit book {bookId} cause it is not exist!");
            }

            UpdateBookData(book, data);
            _booksRepository.UpdateBook(book);
        }

        public Stream GetCover(int bookId)
        {
            var book = _booksRepository.GetBook(bookId);
            if (book == null)
            {
                throw new ArgumentException($"Book with id {bookId} not found!");
            }

            if (book.Cover == null)
            {
                throw new ArgumentException($"Book with id {bookId} don't have cover!");
            }

            return _filesRepository.GetFileContents(book.Cover.Id);
        }

        async public Task SaveCover(int bookId, Stream stream)
        {
            var book = _booksRepository.GetBook(bookId);
            if (book == null)
            {
                throw new ArgumentException($"Book with id {bookId} not found!");
            }

            var file = new BookEditorDemo.Models.File();
            await _filesRepository.AddFile(file, stream);

            book.Cover = file;
            _booksRepository.UpdateBook(book);
        }

        private void UpdateBookData(Book book, UpdateBookData data)
        {
            UpdateValue((value) => book.Title = value, data.Title);
            UpdateValue((value) => book.ISBN = value, data.ISBN);
            UpdateValue((value) => book.PageNumber = (int)value, data.PageNumber);
            UpdateValue((value) => book.Publisher = value, data.Publisher);
            UpdateValue(() => book.PublicationDate = new DateTime(data.PublishYear.Value, 1, 1),
                data.PublishYear);

            if (data.AuthorIds != null)
            {
                var authors = _authorsRepository.GetAuthors(data.AuthorIds);
                book.SetAuthors(authors);
            }
        }

        private void UpdateValue<T>(Action<T> setter, T value)
        {
            if (value != null)
            {
                setter(value);
            }
        }

        private void UpdateValue<T>(Action setter, T value)
        {
            if (value != null)
            {
                setter();
            }
        }

    }
}