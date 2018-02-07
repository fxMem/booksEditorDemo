using BookEditorDemo.Web.Models;
using BookEditorDemo.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookEditorDemo.Web.Controllers
{
    public class AuthorsController: ControllerBase
    {
        private AuthorsService _authorsService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        public IEnumerable<AuthorViewModel> Get()
        {
            return _authorsService.GetAll().Select(a => new AuthorViewModel()
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName
            });
        }
    }
}