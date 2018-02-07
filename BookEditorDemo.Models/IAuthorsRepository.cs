using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public interface IAuthorsRepository
    {
        // For sake of simplicity, I add this "get all" method. Since
        // we don't expect having many authors, this shouldn't be much of a problem.
        // It will require another design in real system though, probably adding searching
        // by lastname functionality.
        List<Author> GetExistingAuthors();

        List<Author> GetAuthors(List<int> authorIds);

        void AddAuthor(Author author);
    }
}
