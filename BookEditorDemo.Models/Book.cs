using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public class Book: Entity
    {
        private List<Author> _authors;

        private string _publisher;
        private string _isn;
        private DateTime _publicationDate;
        private int _pageNumber;
        private string _title;
        private string _firstAuthorFirstName;
        private string _firstAuthorLastName;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                value.CheckRequiredAndThrow(maxLength: 30);
                _title = value;
            }
        }

        public List<Author> Authors
        {
            get { return _authors; }
        }

        // This denormalization may be useful in case of Database storage
        // (as for current in-memory implementation, its not much useful), so we wouldn't need to join Authors table
        // to get books descriptions. Anyway, it depends and will require some profiling.
        public string FirstAuthorFirstName
        {
            get { return Authors.First().FirstName; }
        }

        public string FirstAuthorLastName
        {
            get { return Authors.First().LastName; }
        }

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                value.CheckRangeAndThrow(minValue: 0, maxValue: 10_000);
                _pageNumber = value;
            }
        }

        public DateTime PublicationDate
        {
            get
            {
                return _publicationDate;
            }
            set
            {
                value.Year.CheckRangeAndThrow(minValue: 1800);
                _publicationDate = value;
            }
        }

        public string Publisher
        {
            get
            {
                return _publisher;
            }
            set
            {
                value.CheckOptionalAndThrow(maxLength: 30);
                _publisher = value;
            }
        }

        public string ISBN
        {
            get
            {
                return _isn;
            }
            set
            {
                if (!value.CheckRegex(@"^(97[89])?\d{9}[\dXx]$"))
                {
                    throw new ArgumentException($"ISBN {value} is in incorrect format!");
                }

                _isn = value;
            }
        }

        public File Cover { get; set; }

        public bool HaveCoverImage
        {
            get
            {
                return Cover != null;
            }
        }

        public void SetAuthors(IEnumerable<Author> authors)
        {
            if (authors.Count() == 0)
            {
                throw new ArgumentException("Book should have at least one author!");
            }

            _authors = authors.ToList();
        }

        public Book(string title, 
            int pageNumber, 
            DateTime publishDate, 
            string ISBN, 
            IEnumerable<Author> authors, 
            string publisher = null)
        {
            Title = title;
            PageNumber = pageNumber;
            PublicationDate = publishDate;
            this.ISBN = ISBN;
            SetAuthors(authors);
            Publisher = publisher;
        }
    }
}
