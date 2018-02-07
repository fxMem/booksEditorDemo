using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookEditorDemo.Web.ViewModels
{
    public class BookInfoViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public int PublicationYear { get; set; }
    }
}