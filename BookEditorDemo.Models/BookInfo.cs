using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    // Simple and short book description to show user
    public class BookInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public int PublicationYear { get; set; }
    }
}
