using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public class InMemoryAuthosRepository : IAuthorsRepository
    {
        private object _lock = new object();
        private List<Author> _authors = new List<Author>();
        private int _idSequence;

        public InMemoryAuthosRepository()
        {
            AddSampleAuthors();
        }

        public List<Author> GetExistingAuthors()
        {
            return _authors;
        }

        private void AddSampleAuthors()
        {
            AddAuthor("Joanne", "Rowling");
            AddAuthor("Brandon", "Sanderson");
            AddAuthor("Leo", "Tolstoy");
        }

        public List<Author> GetAuthors(List<int> authorIds)
        {
            return _authors.Where(a => authorIds.Contains(a.Id)).ToList();
        }

        private void AddAuthor(string firstName, string lastName)
        {
            
        }

        public void AddAuthor(Author author)
        {
            lock (_lock)
            {
                author.Id = _idSequence++;
                _authors.Add(author);
            }
        }

        
    }
}
