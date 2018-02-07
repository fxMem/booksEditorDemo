using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookEditorDemo.Web.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PublishYear { get; set; }

        public int PageNumber { get; set; }

        public string ISBN { get; set; }

        public string Publisher { get; set; }

        public bool HaveCover { get; set; }

        public List<AuthorViewModel> Authors { get; set; }
    }
}