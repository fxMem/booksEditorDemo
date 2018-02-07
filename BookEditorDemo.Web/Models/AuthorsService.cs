using BookEditorDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookEditorDemo.Web.Models
{
    public class AuthorsService
    {
        private IAuthorsRepository _authorsRepository;

        public AuthorsService(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public List<Author> GetAll()
        {
            return _authorsRepository.GetExistingAuthors();
        }

        public void AddAuthor(CreateAuthorData data)
        {
            var author = new Author(data.FirstName, data.LastName);
            _authorsRepository.AddAuthor(author);
        }
    }
}